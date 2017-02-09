using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace AskAndAnswer.ClassCode
{
    /// <summary>
    /// A class holding data obtained for a Part Number from a file
    /// </summary>
    public class OTSPartInfo
    {
        string m_pn = "";
        public string PartNumber
        {
            get { return m_pn; }
        }
        string m_desc = "";
        public string Description
        {
            get { return m_desc; }
        }
        string m_desc2 = "";
        public string Description2
        {
            get { return m_desc2; }
        }
        string m_requestor = "";
        public string Requestor
        {
            get { return m_requestor; }
        }
        string m_type = "";
        public string PartType
        {
            get { return m_type; }
        }
        string m_mfrname = "";
        public string MfrName
        {
            get { return m_mfrname; }
        }
        string m_mfrpn = "";
        public string MFRPN
        {
            get { return m_mfrpn; }
        }
        string m_pnstatus = "";
        public string PNStatus
        {
            get { return m_pnstatus; }
            set { m_pnstatus = value; }
        }
        string m_vendorpnstatus = "";
        public string VendorPNStatus
        {
            get { return m_vendorpnstatus; }
            set { m_vendorpnstatus = value; }
        }
        Int64 m_PNID = -1;
        public Int64 PNID
        {
            get { return m_PNID; }
        }
        Int64 m_VendorPNID = -1;
        public Int64 VendorPNID
        {
            get { return m_VendorPNID; }
        }
        string m_envStatus = "";
        public string EnvStatus
        {
            get { return m_envStatus; }
            set { m_envStatus = value; }
        }

        string m_datasheetURL = "";
        public string DataSheetURL
        {
            get {
                    if (m_datasheetURL == "")
                    {
                        return "LINK NOT YET AVAILABLE";
                    } else
                    {
                        return m_datasheetURL;
                    }
                 }
        }

        decimal m_lvc = 0;
        public decimal LowVolCost
        {
            get { return m_lvc; }
            set { m_lvc = value; }
        }

        decimal m_hvc = 0;
        public decimal HighVolCost
        {
            get { return m_hvc; }
            set { m_hvc = value; }
        }

        decimal m_ec = 0;
        public decimal EngCost
        {
            get { return m_ec; }
            set { m_ec = value; }
        }

        decimal m_height = 0;
        public decimal Height
        {
            get { return m_height; }
            set { m_height = value; }
        }
        //Constructor used for basic (OTS info only) object
        public OTSPartInfo(Int64 PNID, Int64 VendorPNID, string pn, string desc, string desc2, string requestor, string type, string mfrname,
            string mfrpn)
        {
            m_PNID = PNID;
            m_VendorPNID = VendorPNID;
            m_pn = pn;
            m_desc = desc;
            m_desc2 = desc2;
            m_requestor = requestor;
            m_type = type;
            m_mfrname = mfrname;
            m_mfrpn = mfrpn;
        }

        public OTSPartInfo(Int64 PNID, Int64 VendorPNID, string pn, string desc, string desc2, string requestor, string type, string mfrname,
                string mfrpn, string pnstatus, string vendorpnstatus, string estatus, string datasheeturl, decimal lowvolcost, decimal highvolcost,
                decimal engcost, decimal maxheight)
        {
            m_PNID = PNID;
            m_VendorPNID = VendorPNID;
            m_pn = pn;
            m_desc = desc;
            m_desc2 = desc2;
            m_requestor = requestor;
            m_type = type;
            m_mfrname = mfrname;
            m_mfrpn = mfrpn;
            m_pnstatus = pnstatus;
            m_vendorpnstatus = vendorpnstatus;
            m_envStatus = estatus;
            m_datasheetURL = datasheeturl;
            m_lvc = lowvolcost;
            m_hvc = highvolcost;
            m_ec = engcost;
            m_height = maxheight;
        }
    }
    public class clsOTSUploader
    {

        public clsOTSUploader()
        {

        }

        private enum uploadType { BULK, REPORT };

        private uploadType m_uType = uploadType.BULK;

        /// <summary>
        /// Process the file to import its contents into the OTS database.
        /// Progress is written to a response object, which can be used for real-time updates if client
        /// processes server-sent events.
        /// The method also returns an html string that can be used to summarize what happened during logging.
        /// </summary>
        /// <param name="resp">SSE Response Object</param>
        /// <param name="fd">Uploaded file directcory</param>
        /// <param name="fn">Uploaded file name</param>
        public string ProcessFile(SSERelay resp, string fd, string fn)
        {

            //lists to hold changed parts
            List<string> lstNewParts = new List<string>();
            List<string> lstAdjParts = new List<string>();
            List<string> lstAVLAdjParts = new List<string>();
            //This dictionary maps the OTSPN ID to a list of new Vendor Part Numbers added for that ID
            Dictionary<Int64,List<string>> dctIDtoNewVPNs = new Dictionary<Int64,List<string>>();
            //List of newly added vendor part numbers

            //Dictionary holding all Part Types
            Dictionary<string, List<string>> dPTypes = new Dictionary<string, List<string>>();

            //Variables to hold the values we will extract from the excel file
            string pn = "";
            string desc = "";
            string desc2 = "";
            string requestor = "";
            string type = "";
            string mfrname = "";
            string mfrpn = "";
            string pnstatus = "";
            string vendorpnstatus = "";
            int currR = -1;

            XL x = null;
            clsFileUtil f = new clsFileUtil("");
            string fullServerFileName = "";
            //Define some Excel column numbers
            int colPN = -1;
            int colDesc = -1;
            int colDesc2 = -1;
            int colRequestor = -1;
            int colType = -1;
            int colMfrName = -1;
            int colMfrPN = -1;
            int colPNStatus = -1;
            int colVendorPNStatus = -1;

            //Used for Title Case for names
            TextInfo tI = new CultureInfo("en-US", false).TextInfo;

            clsDB xDB = new clsDB();

            try
            {
                f.MakeDirectory(fd + "\\");
                fullServerFileName = fd + "\\" + fn;
                resp.send("Start Processing File '" + fn + "'...");

                //create a New XL object
                x = new XL(fullServerFileName, false);


                if (x == null)
                {
                    resp.send("Could not read file.  Please make sure you provided a valid file.");
                    return resp.GetMessageBlock();
                }
                else if (x.ErrMsg != "")
                {
                    resp.send("An Error was encountered with the file you uploaded: " +
                        x.ErrMsg.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()));
                    return resp.GetMessageBlock();
                }

                //Continue with upload if no errors happened during initialization
                x.SetWorkSheet("");
                //Determine the positions of certain columns by inspecting the header row
                currR = x.FirstRow;
                while (x.GetCellValue(currR, x.FirstCol)== "")
                {
                    currR++;
                }
                //We are at the first (therefore, header) row; determine the column indices based on the values
                //Below, first case indicates we are dealing with a REPORT File
                //second case indicates we are dealing with a substandard file (becuase it is missing information)
                int c = x.FirstCol;
                
                while (x.GetCellValue(currR, c) != "")
                {
                    //NOTE: The if (colIndex < 0) statements guarantee we take the first Column Header
                    string temp = x.GetCellValue(currR, c);
                    switch (x.GetCellValue(currR,c).ToLower().Trim().Replace("_","").Replace(" ","")) {
                        case "itemnumber":
                        case "number":
                            if (colPN < 0)
                            {
                                colPN = c;
                            }  
                            break;
                        case "description":
                            if (colDesc < 0)
                            {
                                colDesc = c;
                            }
                            break;
                        case "description2":
                            if (colDesc2 < 0)
                            {
                                colDesc2 = c;
                            }
                            break;
                        case "designengineer":
                            if (colRequestor < 0)
                            {
                                colRequestor = c;
                            }
                            break;
                        case "subclass":
                            if (colType < 0)
                            {
                                colType = c;
                            }
                            break;
                        case "manufacturersname":
                        case "manufacturers.mfr.name":
                            if (colMfrName < 0)
                            {
                                colMfrName = c;
                            }
                            break;
                        case "manufacturerspartnumber":
                        case "manufacturers.mfr.partnumber":
                            if (colMfrPN < 0)
                            {
                                colMfrPN = c;
                            }
                            break;
                        case "lifecyclephase":
                        case "lifecycle(page3)":
                            if (colPNStatus < 0)
                            {
                                colPNStatus = c;
                            }
                            break;
                        case "mfr.partlifecyclephase":
                        case "manufacturers.mfr.partlifecyclephase":
                            if (colVendorPNStatus < 0)
                            {
                                colVendorPNStatus = c;
                            }
                            break;
                        default:
                            break;
                    }
                    c++;
                }
                //The value of the column indexes tells us if we have a valid file
                if (colPN == -1 || colDesc == -1 || colMfrName == -1 || colMfrPN == -1 || colPNStatus == -1 || colVendorPNStatus == -1)
                {
                    resp.send("File '" + fn + "' is missing at least one element in the header to identify column indexes.  Current index values:" + DynControls.html_linebreak_string() +
                        "Part Number Column Index: " + colPN.ToString() + DynControls.html_linebreak_string() +
                        "Description Column Index: " + colDesc.ToString() + DynControls.html_linebreak_string() +
                        "Manufacturer Column Index: " + colMfrName.ToString() + DynControls.html_linebreak_string() +
                        "Manufacturer PN Column Index: " + colMfrPN.ToString() + DynControls.html_linebreak_string() +
                        "PN LifeCycle Phase Column Index: " + colPNStatus.ToString() + DynControls.html_linebreak_string() +
                        "Vendor PN LifeCycle Phase Column Index: " + colVendorPNStatus.ToString() + DynControls.html_linebreak_string() +
                        "Data from this file cannot be imported.");
                    return resp.GetMessageBlock();
                }
                else
                {
                    //Advance to next row
                    currR++;
                }
                while (x.GetCellValue(currR, colPN) != "")
                {
                    Boolean blDoInsert = true;
                    pn = x.GetCellValue(currR, colPN).Trim();
                    desc = x.GetCellValue(currR, colDesc).Trim();
                    if (desc.Split(',').GetUpperBound(0) > 3)
                    {
                        string[] arr = desc.Split(',');
                        string odesc = arr[0] + "," + arr[1] + "," + arr[2] + "," + arr[3];
                        desc2 = desc.Replace(odesc + ",", "");
                        desc = odesc;
                    }
                    else
                    {
                        desc2 = "";
                    }
                    if (colRequestor == -1)
                    {
                        requestor = "UNKNOWN";
                    } else
                    {
                        requestor = x.GetCellValue(currR, colRequestor).ToLower().Trim();
                        if (requestor.Split(' ').GetUpperBound(0)<1)
                        {
                            requestor = "UNKNOWN";
                        } else
                        {
                            //Ensure capitalization handled correctly
                            requestor = tI.ToTitleCase(requestor);
                        }
                    }
                    if (colType == -1)
                    {
                        type = "UNKNOWN";
                    } else
                    {
                        type = x.GetCellValue(currR, colType).ToUpper().Trim();
                        if (type == "")
                        {
                            type = "UNKNOWN";
                        }
                    }
                    
                    mfrname = x.GetCellValue(currR, colMfrName).ToUpper().Trim();
                    mfrpn = x.GetCellValue(currR, colMfrPN).ToUpper().Trim();
                    pnstatus = x.GetCellValue(currR, colPNStatus).ToUpper().Trim();
                    vendorpnstatus = x.GetCellValue(currR, colVendorPNStatus).ToUpper().Trim();
                    if (pn == "")
                    {
                        resp.send("File '" + fn + "' Row " + currR.ToString() + " is blank for Part Number; unable to import data for row " + currR + ".");
                        blDoInsert = false;
                    }
                    else if (mfrname == "")
                    {
                        resp.send("File '" + fn + "' Row " + currR.ToString() + " is blank for Manufacturer Name; unable to import data for " + pn + " (Row " + currR + ").");
                        blDoInsert = false;
                    }
                    else if (mfrpn == "")
                    {
                        resp.send("File '" + fn + "' Row " + currR.ToString() + " is blank for Manufacturer Part Number; unable to import data for " + pn + " (Row " + currR + ").");
                        blDoInsert = false;
                    }
                    else if (pnstatus == "")
                    {
                        resp.send("File '" + fn + "' Row " + currR.ToString() + " is blank for Part Number Status; unable to import data for " + pn + " (Row " + currR + ").");
                        blDoInsert = false;
                    }
                    else if (vendorpnstatus == "")
                    {
                        resp.send("File '" + fn + "' Row " + currR.ToString() + " is blank for Vendor Part Number Status; unable to import data for " + pn + " (Row " + currR + ").");
                        blDoInsert = false;
                    }

                    if (vendorpnstatus.ToLower().StartsWith("obs"))
                    {
                        vendorpnstatus = "OBSOLETE";
                    } else
                    {
                        vendorpnstatus = "APPROVED";
                    }
                    //Log the part type
                    if (type.ToUpper()!="UNKNOWN")
                    {
                        if (!dPTypes.ContainsKey(type))
                        {
                            dPTypes.Add(type, new List<string>());
                        }
                        dPTypes[type].Add(pn);
                    }

                    //What do we do with this pn?
                    //we need a variable to hold its ID, if it already exists in the database
                    Int64 pnID = -1;
                    
                    //if pn exists in the database, we are doing some kind of edit;
                    //note this covers the condition where the file contains two or more rows with the same pn
                    //(because of more than one vendor)
                    if (pn != "" && PNExists(pn, ref pnID))
                    {
                        OTSPartInfo OPI = new OTSPartInfo(pnID, -1, pn, desc, desc2, requestor, type, 
                            mfrname, mfrpn,pnstatus,vendorpnstatus,"","",0,0,0,0);
                        List<string> tmpList = new List<string>();
                        if (!dctIDtoNewVPNs.ContainsKey(pnID))
                        {
                            List<string> tmpLst = new List<string>();
                            dctIDtoNewVPNs.Add(pnID, tmpLst);
                        } else
                        {
                            tmpList = dctIDtoNewVPNs[pnID];
                        }
                        resp.send(ReconcilePartNumberInfo(OPI, ref lstAdjParts, ref lstAVLAdjParts,ref tmpList));
                        dctIDtoNewVPNs[pnID] = tmpList;
                    }
                    else if (blDoInsert)
                    //otherwise we are adding a new part number (assuming no error, as indicated by blDoInsert)
                    {
                        List<SqlParameter> ps = new List<SqlParameter>();
                        SqlCommand cmd = new SqlCommand();
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strPARTNUMBER), pn));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strNAME), requestor));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strVENDOR), mfrname));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strVENDORPARTNUMBER), mfrpn));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strDESCRIPTION), desc));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strDESCRIPTION2), desc2));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strTYPE), type));
                        ps.Add(new SqlParameter(xDB.makeParameterName(DBK.strPRODUCT), ""));
                        ps.Add(xDB.makeReturnParameter(System.Data.SqlDbType.Int));
                        //Since we are using a return parameter, the cmd object's command type must be stored procedure
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        int insertResult = -1;
                        using (xDB.OpenConnection())
                        {
                            xDB.ExecuteSP(DBK.SP.spOTSINSERTNEWPARTNUMBER2, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                            insertResult = (int)cmd.Parameters["@returnVal"].Value;
                            if (insertResult == 0)
                            {
                                resp.send("Successfully associated " + pn + " with " + mfrpn + " in database.");
                                if (!lstNewParts.Contains(pn))
                                {
                                    lstNewParts.Add(pn);
                                }
                            }
                            else if (insertResult == -100)
                            {
                                resp.send("Unable to insert information for " + pn + "-- Part Type '" + type + "' has not been approved for insertion in the database.");
                            }
                            else
                            {
                                resp.send("Unable to insert " + pn + " with MFGPN " + mfrpn + " to database.  SQL Server Return Code: " + insertResult);
                            }
                        }
                    }
                    //update
                    currR++;
                }

                //Deal with each otsPN for which we inserted a new vendor part number
                foreach (Int64 pID in dctIDtoNewVPNs.Keys )
                {
                    List<string> tmpList = dctIDtoNewVPNs[pID];
                    if (tmpList.Count>0)
                    {
                        SqlCommand cmd = new SqlCommand();
                        List<SqlParameter> ps = new List<SqlParameter>();
                        clsDB tmpDB = new clsDB();
                        ps.Add(new SqlParameter("@" + DBK.ID, pID));
                        ps.Add(new SqlParameter("@" + DBK.keyUPDATEDBY, 7));
                        ps.Add(xDB.makeOutputParameter("@changed", System.Data.SqlDbType.Bit));
                        ps.Add(new SqlParameter("@newVendorPNs", String.Join(AAAK.vbCRLF, tmpList.ToArray()).Replace("ADJONLY","").Replace(",,",",")));
                        if (ps[ps.Count - 1].Value.ToString().StartsWith(","))
                        {
                            ps[ps.Count - 1].Value = ps[ps.Count - 1].Value.ToString().Substring(1);
                        }
                        if (ps[ps.Count - 1].Value.ToString().EndsWith(","))
                        {
                            ps[ps.Count - 1].Value = ps[ps.Count - 1].Value.ToString().Substring(0, ps[ps.Count - 1].Value.ToString().Length - 1);
                        }
                        using (xDB.OpenConnection())
                        {
                            object r = xDB.ExecuteSP(DBK.SP.spOTSUPDATEPARTSBASEDINAVL, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                            if (r.ToString().Contains(" "))
                            {
                                resp.send("Error when executing stored procedure: " + r.ToString() + 
                                    "; unable to update OTS Properties for Database ID " + pID + ".");
                            }
                            else if (Convert.ToByte(cmd.Parameters["@changed"].Value) == 1)
                            {
                                resp.send("Updated OTS Properties for Database ID " + pID + ".");
                            }
                            cmd.Parameters.Clear();
                        }
                    }
                } 

                //Report any uncreated Part Types (since these parts were simply filed as unknown)
                List<string> lstCsvTypes = new List<string>();
                StringBuilder newTypes = new StringBuilder();
                foreach (string k in dPTypes.Keys)
                {
                    lstCsvTypes.Add(k);
                }
                SqlCommand cmdPT = new SqlCommand();
                List<SqlParameter> psPT = new List<SqlParameter>();
                clsDB myDB = new clsDB();
                psPT.Add(new SqlParameter("@csvPartTypes", String.Join(",", lstCsvTypes.ToArray())));
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSFINDUNDEFINEDPARTTYPES, psPT, clsDB.SPExMode.READER, ref cmdPT))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            while (dR.Read())
                            {
                                if (myDB.Fld2Str(dR["pn"]) !="")
                                {
                                    newTypes.Append(dR["pn"].ToString() + DynControls.html_linebreak_string());
                                }
                            }
                        }
                    }
                }
                if (newTypes.Length>0)
                {
                    newTypes.Insert(0, "The following Part Types are not yet defined in the database:" + DynControls.html_linebreak_string());
                    newTypes.Append("The Part Numbers associated with these Types above were assigned type 'UNKNOWN'.  Define these Part Types and re-import " +
                    "to update the Part Type in the database." + DynControls.html_linebreak_string());
                    resp.send(newTypes.ToString());
                }
                if (lstAdjParts.Count>0)
                {
                    resp.send("The following parts have been modified:" + DynControls.html_linebreak_string() +
                        String.Join(DynControls.html_linebreak_string(), lstAdjParts.ToArray()));
                }
                if (lstAVLAdjParts.Count > 0)
                {
                    resp.send("The following parts have had had one or more aspects of their AVL modified:" + DynControls.html_linebreak_string() +
                        String.Join(DynControls.html_linebreak_string(), lstAVLAdjParts.ToArray()));
                }
                if (lstNewParts.Count > 0)
                {
                    resp.send("The following parts have been inserted into the Database:" + DynControls.html_linebreak_string() +
                        String.Join(DynControls.html_linebreak_string(), lstNewParts.ToArray()));
                }
                resp.send("Finished processing file '" + fn + "'.");
                //Put the stringbuilder in divMsg
                return resp.GetMessageBlock();
            }

            catch (Exception ex)
            {
                string errMsg = ex.GetType().ToString() + ex.Message + ex.StackTrace;
                resp.send("data: " + errMsg.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()));
                return resp.GetMessageBlock();
            }
            finally
            {
                try
                {
                    x.CloseDispose();
                }
                catch (Exception eM)
                {
                    string errM = eM.Message + eM.StackTrace;
                }
                try
                {
                    System.IO.FileInfo fl = new System.IO.FileInfo(fullServerFileName);
                    fl.Delete();
                }
                catch (Exception eM)
                {
                    string errM = eM.Message + eM.StackTrace;
                }

            }
        }

        /// <summary>
        /// Reconciles the information in the OTSPartIfor object, which has been extracted from a file,
        /// with the information in the database for the part with pnID.
        /// Result of the reconciliation is retured in a string
        /// </summary>
        /// <param name="opi">object containing latest OTS Part Number Info, usually from a file</param>
        /// <param name="sbNewParts">Stringbuilder containing list of parts that have been inserted</param>
        /// <param name="sbAdjustedParts">Stringbuilder containing list of parts that have been adjusted (not the AVL)</param>
        /// <param name="sbAVLAdjustedParts">Strinbuilder containing list of parts that have had their AVL adjusted</param>
        /// <param name="lstNewVendorPartNumbers">Vendor Part Numbers that have been added</param>
        /// <returns></returns>
        public string ReconcilePartNumberInfo(OTSPartInfo opi,
            ref List<string> lstAdjustedParts,
            ref List<string> lstAVLAdjustedParts,
            ref List<string> lstNewVendorPartNumbers)
        {
            //This variable will hold information for the part from the database
            OTSPartInfo currentPNInfo = null;
            //This dictionary will hold information for each vendor pn in the AVL from the database
            Dictionary<string, OTSPartInfo> dctCurrentVendorOPI = new Dictionary<string, OTSPartInfo>();
            Boolean blChangeDetected = false;
            StringBuilder sb = new StringBuilder();
            try
            {
                //Open a data reader for the id to the database
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@pnID", opi.PNID));
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSGETPNINFO, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        //This stored proc returns two result sets.
                        //The first set contains information about the part.
                        //The second set contains information about all Vendor PNs that report to the part.
                        int resultSetIndex = 0;
                        do
                        {
                            if (dR != null && dR.HasRows)
                            {
                                if (resultSetIndex == 0)
                                {
                                    //Get current information for the part;
                                    dR.Read();
                                    currentPNInfo = new OTSPartInfo((Int64)dR[DBK.ID],
                                                                    -1,
                                                                    myDB.Fld2Str(dR[DBK.strPARTNUMBER]),
                                                                    myDB.Fld2Str(dR[DBK.strDESCRIPTION]),
                                                                    myDB.Fld2Str(dR[DBK.strDESCRIPTION2]),
                                                                    myDB.Fld2Str(dR[DBK.strNAME]),
                                                                    myDB.Fld2Str(dR[DBK.strTYPE]),
                                                                    "", "");
                                }
                                else
                                {
                                    //Get current information for the vendor parts associated with this part
                                    while (dR.Read())
                                    {
                                        string k = myDB.Fld2Str(dR[DBK.strVENDOR]) + "_" + myDB.Fld2Str(dR[DBK.strVENDORPARTNUMBER]);
                                        if (!dctCurrentVendorOPI.ContainsKey(k))
                                        {
                                            dctCurrentVendorOPI.Add(k,
                                                new OTSPartInfo(
                                                (Int64)dR[DBK.ID],
                                                (Int64)dR[DBK.SP_COLALIAS.VENDORPNID],
                                                "", 
                                                "",
                                                "",
                                                "",
                                                "",
                                                k.Split('_')[0],
                                                k.Split('_')[1],
                                                "",
                                                myDB.Fld2Str(dR[DBK.strSTATUS]),
                                                myDB.Fld2Str(dR[DBK.strECODENAME]),
                                                myDB.Fld2Str(dR[DBK.strDATASHEETURL]),
                                                Decimal.Parse(myDB.Fld2Str(dR[DBK.decLOWVOLCOST], "0.0")),
                                                Decimal.Parse(myDB.Fld2Str(dR[DBK.decHIGHVOLCOST], "0.0")),
                                                Decimal.Parse(myDB.Fld2Str(dR[DBK.decENGCOST], "0.0")),
                                                Decimal.Parse(myDB.Fld2Str(dR[DBK.decMAXHEIGHT], "0.0"))
                                                )
                                                );
                                        }

                                    }
                                }

                            }
                            else
                            {
                                return ("Unable to get information from database for " + opi.PartNumber + ", ID = " + opi.PNID);
                            }
                            resultSetIndex = resultSetIndex + 1;
                        } while (dR.NextResult());
                    }
                }

                //Reset the variables
                myDB = new clsDB();
                cmd = new SqlCommand();
                ps = new List<SqlParameter>();
                //Process the information we just obtained
                if (opi.Description.ToLower() != currentPNInfo.Description.ToLower() ||
                    opi.Description2.ToLower() != currentPNInfo.Description2.ToLower() ||
                    opi.Requestor.ToLower() != currentPNInfo.Requestor.ToLower() ||
                    opi.PartType.ToLower() != currentPNInfo.PartType.ToLower())
                {
                    //Update currentPNInfo in database with info from opi
                    blChangeDetected = true;
                    //Call spOTSReconcilePartsTable
                    ps.Add(new SqlParameter("@" + DBK.ID, opi.PNID));
                    ps.Add(new SqlParameter("@" + DBK.strDESCRIPTION, opi.Description));
                    ps.Add(new SqlParameter("@" + DBK.strDESCRIPTION2, opi.Description2));
                    ps.Add(new SqlParameter("@" + DBK.strTYPE, opi.PartType));
                    ps.Add(new SqlParameter("@" + DBK.strNAME, opi.Requestor));
                    ps.Add(myDB.makeOutputParameter("@changed", System.Data.SqlDbType.Bit));
                    using (myDB.OpenConnection())
                    {
                        using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSRECONCILEPARTSTABLE, ps, clsDB.SPExMode.READER, ref cmd))
                        {
                            if (Convert.ToString(cmd.Parameters["@changed"].Value) == "0" || !dR.HasRows)
                            {
                                sb.Append("No changes detected for " + opi.PartNumber + " (Database ID=" + opi.PNID + ").");
                            } else if (Convert.ToString(cmd.Parameters["@changed"].Value) == "0")
                            {
                                sb.Append("Part Number " + opi.PartNumber + " was not imported because Part Type " + opi.PartType + " has not been approved for Database Insertion.");
                            } else
                            {
                                sb.Append("Changes made in otsParts for " + opi.PartNumber + " (Database ID=" + opi.PNID + "):" + DynControls.html_linebreak_string());
                                if (!lstAdjustedParts.Contains(opi.PartNumber))
                                {
                                    lstAdjustedParts.Add(opi.PartNumber);
                                }
                                dR.NextResult();    //ignore first result
                                dR.Read();
                                if (dR.HasRows)
                                {
                                    if (myDB.Fld2Str(dR[DBK.DESCRIPTION_CHANGE])!="")
                                    {
                                        sb.Append("Description Change: " + myDB.Fld2Str(dR[DBK.DESCRIPTION_CHANGE]) + DynControls.html_linebreak_string());
                                    }
                                    if (myDB.Fld2Str(dR[DBK.DESCRIPTION2_CHANGE]) != "")
                                    {
                                        sb.Append("Description2 Change: " + myDB.Fld2Str(dR[DBK.DESCRIPTION2_CHANGE]) + DynControls.html_linebreak_string());
                                    }
                                    if (myDB.Fld2Str(dR[DBK.PART_TYPE_CHANGE]) != "")
                                    {
                                        sb.Append("Part Type Change: " + myDB.Fld2Str(dR[DBK.PART_TYPE_CHANGE]) + DynControls.html_linebreak_string());
                                    }
                                    if (myDB.Fld2Str(dR[DBK.PART_SUBTYPE_CHANGE]) != "")
                                    {
                                        sb.Append("Part SubType Change: " + myDB.Fld2Str(dR[DBK.PART_SUBTYPE_CHANGE]) + DynControls.html_linebreak_string());
                                    }                                   
                                } 
                                //Following are not recorded under part history
                                if (opi.Requestor.ToLower() != currentPNInfo.Requestor.ToLower())
                                {
                                    sb.Append("Requestor Updated: FROM[" + currentPNInfo.Requestor + "]TO[" + opi.Requestor  + "]" + DynControls.html_linebreak_string());
                                }                             
                            }
                        }

                    }
                    //Reset the variables
                    myDB = new clsDB();
                    cmd = new SqlCommand();
                    ps = new List<SqlParameter>();
                }

                if (dctCurrentVendorOPI.ContainsKey(opi.MfrName + "_" + opi.MFRPN))
                {
                    //This part already exists in the AVL; adjust it
                    OTSPartInfo targetOPI = dctCurrentVendorOPI[opi.MfrName + "_" + opi.MFRPN];
                    //Ensure valid values for certain conditions
                    if( opi.LowVolCost==0)
                    {
                        opi.LowVolCost = targetOPI.LowVolCost;
                    }
                    if (opi.HighVolCost == 0)
                    {
                        opi.HighVolCost = targetOPI.HighVolCost;
                    }
                    if (opi.EngCost == 0)
                    {
                        opi.EngCost = targetOPI.EngCost;
                    }
                    if (opi.Height == 0)
                    {
                        opi.Height = targetOPI.Height;
                    }
                    if (opi.EnvStatus == "")
                    {
                        opi.EnvStatus = targetOPI.EnvStatus;
                    }
                    if (opi.EnvStatus.ToLower()=="none")
                    {
                        opi.VendorPNStatus = "SUBMITTED";
                    }
                    else if (opi.VendorPNStatus == "")
                    {
                        opi.VendorPNStatus = targetOPI.VendorPNStatus;
                    }


                    if (opi.VendorPNStatus.ToLower() != targetOPI.VendorPNStatus.ToLower() ||
                        opi.EnvStatus.ToLower() != targetOPI.EnvStatus.ToLower() ||
                        opi.DataSheetURL.ToLower() != targetOPI.DataSheetURL.ToLower() ||
                        opi.LowVolCost != targetOPI.LowVolCost ||
                        opi.HighVolCost != targetOPI.HighVolCost ||
                        opi.EngCost != targetOPI.EngCost ||
                        opi.Height != targetOPI.Height)
                    {
                        //Update targetOPI info in database with info from opi
                        blChangeDetected = true;
                        //Call spOTSReconcileVendorPNTable
                        ps.Add(new SqlParameter("@" + DBK.ID, targetOPI.VendorPNID));
                        ps.Add(new SqlParameter("@" + DBK.strVENDORPARTNUMBER, opi.MFRPN));
                        //For decimal values (cost, height), zero is meaningless; treat it as null.
                        if (opi.LowVolCost == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decLOWVOLCOST, DBNull.Value));
                        }
                        else
                        {
                            SqlParameter lvc = new SqlParameter("@" + DBK.decLOWVOLCOST, Convert.ToDecimal(opi.LowVolCost));
                            lvc.Precision = 18;
                            lvc.Scale = 6;
                            ps.Add(lvc);
                        }
                        if (opi.HighVolCost == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decHIGHVOLCOST, DBNull.Value));
                        }
                        else
                        {
                            SqlParameter hvc = new SqlParameter("@" + DBK.decHIGHVOLCOST, Convert.ToDecimal(opi.HighVolCost));
                            hvc.Precision = 18;
                            hvc.Scale = 6;
                            ps.Add(hvc);
                        }
                        if (opi.EngCost == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decENGCOST, DBNull.Value));
                        }
                        else
                        {
                            SqlParameter ec = new SqlParameter("@" + DBK.decENGCOST, Convert.ToDecimal(opi.EngCost));
                            ec.Precision = 18;
                            ec.Scale = 6;
                            ps.Add(ec);
                        }
                        if (opi.Height == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decMAXHEIGHT, DBNull.Value));
                        }
                        else
                        {
                            SqlParameter mh = new SqlParameter("@" + DBK.decMAXHEIGHT, Convert.ToDecimal(opi.Height));
                            mh.Precision = 18;
                            mh.Scale = 6;
                            ps.Add(mh);
                        }
                        ps.Add(new SqlParameter("@" + DBK.strDATASHEETURL, opi.DataSheetURL));
                        ps.Add(new SqlParameter("@" + DBK.strECODENAME, opi.EnvStatus));
                        ps.Add(new SqlParameter("@" + DBK.strVENDOR, opi.MfrName));
                        ps.Add(new SqlParameter("@" + DBK.strSTATUS, opi.VendorPNStatus));
                        ps.Add(new SqlParameter("@" + DBK.keyUPDATEDBY, 7));
                        ps.Add(myDB.makeOutputParameter(DBK.SPVar.intReturnCode, System.Data.SqlDbType.Int));
                        using (myDB.OpenConnection())
                        {

                            object result = myDB.ExecuteSP(DBK.SP.spOTSRECONCILEVENDORPNTABLE, ps, 
                                clsDB.SPExMode.NONQUERY, ref cmd);
                            //We know that if the output parameter is null, an error occurred.
                            //if (Convert.IsDBNull(cmd.Parameters[DBK.SPVar.intReturnCode].Value))
                            if (result.ToString().Contains(" "))
                            {
                                sb.Append("<p>Unable to execute stored procedure to update values for Vendor Part Number " +
                                    Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) + "</p>" +
                                        "<p>Error Value: " + result.ToString() + "</p>");
                            }
                            else
                            {
                                switch (Convert.ToInt32(cmd.Parameters[DBK.SPVar.intReturnCode].Value))
                                {
                                    case -1:
                                        //Function did not execute successfully.
                                        sb.Append("<p>Unable to change Vendor Part Number " +
                                                Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                " for this OTS Part Number.</p>");

                                        break;
                                    case 2:
                                        //No changes detected for this part number.
                                        sb.Append("<p>No changes were detected in the information you submitted for Vendor Part Number " +
                                                Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) + "</p>" +
                                                "<p>Contact Alberto Campos if this is incorrect</p>");

                                        break;
                                    case 0:
                                    case 1:
                                        if (Convert.ToInt32(cmd.Parameters[DBK.SPVar.intReturnCode].Value) == 1)
                                        {
                                            sb.Append("<p>Vendor Part Number " +
                                                    Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                    " has been updated in the database, but a record of this change was not recorded in the database.</p>" +
                                                    "<p>An email was sent to all Users affected by this change.</p>" +
                                                    "<p>An email was sent to the programming team to investigate why the change was not logged.</p>");
                                        }
                                        else
                                        {
                                            sb.Append("<p>Vendor Part Number " +
                                                    Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                    " has been updated in the database.  An email was sent to all Users affected by this change.</p>");
                                        }
                                        if (!lstAVLAdjustedParts.Contains(opi.PartNumber))
                                        {
                                            lstAVLAdjustedParts.Add(opi.PartNumber);
                                        }
                                        if (!lstNewVendorPartNumbers.Contains("ADJONLY"))
                                        {
                                            lstNewVendorPartNumbers.Add("ADJONLY");
                                        }
                                        break;
                                    default:
                                        sb.Append("<p>Unrecognized code returned from database: " + Convert.ToString(cmd.Parameters[DBK.SPVar.intReturnCode].Value) + "</p>");
                                        break;
                                }
                            }

                        }
                    }

                } else
                {
                    //We are adding to the AVL
                    blChangeDetected = true;
                    ps.Add(new SqlParameter("@ots" + DBK.ID, opi.PNID));
                    ps.Add(new SqlParameter("@" + DBK.strVENDORPARTNUMBER, opi.MFRPN));
                    ps.Add(new SqlParameter("@" + DBK.strVENDOR, opi.MfrName));
                    ps.Add(myDB.makeOutputParameter(DBK.SPVar.intReturnCode, System.Data.SqlDbType.Int));

                    //Add the part
                    using (myDB.OpenConnection())
                    {
                        myDB.ExecuteSP(DBK.SP.spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                        switch (Convert.ToInt32(cmd.Parameters[DBK.SPVar.intReturnCode].Value))
                        {
                            case -1:
                                //Function did not execute successfully.
                                sb.Append("ERROR: Unable to add Vendor Part Number " +
                                        Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                        " to OTS Part Number " + opi.PartNumber + ".");

                                break;
                            case 1:
                                //Part number already associated with this OTS Part Number.
                                sb.Append("ERROR: Vendor Part Number " +
                                        Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) + " is already associated with Part Number" + opi.PartNumber + "." +
                                        "  This program should not have attempted to add this to the AVL.");
                                break;
                            case 0:
                                sb.Append("<p>Vendor Part Number " +
                                        Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                        " was added to OTS Part Number " + opi.PartNumber + ".  Should send email to any affected users (but not yet implemented).");
                                if (!lstAVLAdjustedParts.Contains(opi.PartNumber))
                                {
                                    lstAVLAdjustedParts.Add(opi.PartNumber);
                                }
                                if (!lstNewVendorPartNumbers.Contains(opi.MFRPN))
                                {
                                    lstNewVendorPartNumbers.Add(opi.MFRPN);
                                }
                                break;
                            default:
                                sb.Append("ERROR: Unable to add Vendor Part Number " +
                                            Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                            " to OTS Part Number " + opi.PartNumber + "." + DynControls.html_linebreak_string() +
                                            "Unrecognized code returned from database: " + 
                                            Convert.ToString(cmd.Parameters[DBK.SPVar.intReturnCode].Value));
                                break;
                        } //end switch statement
                    } //DB connection closed
                }
                if (sb.Length==0)
                {
                    sb.Append("No changes detected for " + opi.PartNumber + " (Database ID=" + opi.PNID + ").");
                }
                return sb.ToString();
            } catch (Exception ex)
            {
                return ex.Message + DynControls.html_linebreak_string() +
                    ex.StackTrace.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            }
        }
        /// <summary>
        /// Returns TRUE if pn exists in Database
        /// </summary>
        /// <param name="pn">The Part Number we are searching for</param>
        /// <param name="pnID">ID of the Part Number in the database</param>
        /// <returns></returns>
        public Boolean PNExists(string pn, ref Int64 pnID)
        {
            try
            {
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@csvPN", pn));
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSGETPNIDS, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            dR.Read();
                            if (dR[DBK.ID] == System.DBNull.Value)
                            {
                                pnID = -1;
                                return false;
                            } else
                            {
                                pnID = Int64.Parse(dR[DBK.ID].ToString());
                                return true;
                            }

                        } else
                        {
                            pnID = -1;
                            return false;
                        }
                    }
                }
            } catch (Exception ex)
            {
                string errMsg = ex.Message + ex.StackTrace;
                return true;
            }
        }
    }
}