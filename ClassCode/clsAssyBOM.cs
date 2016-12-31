using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using DB;
using System.Data.SqlClient;
namespace AskAndAnswer.ClassCode

{
    public class clsAssyBOM
    {
        private string m_topLevelName;
        /// <summary>
        /// Top level name of the assembly bom
        /// </summary>
        public string TopLevelName
        {
            get
            {
                return m_topLevelName;
            }
            set
            {
                m_topLevelName = value;
            }
        }

        private string m_assyPN;
        /// <summary>
        /// Part Number of the Assembly BOM
        /// </summary>
        public string AssyPN
        {
            get
            {
                return m_assyPN;
            }
            set
            {
                m_assyPN = value;
            }
        }

        private string m_assyRev;
        /// <summary>
        /// Revision of the product
        /// </summary>
        public string AssyRev
        {
            get
            {
                return m_assyRev;
            }
            set
            {
                m_assyRev = value;
            }
        }
        private int m_bomRev;
        /// <summary>
        /// Revision of the product BOM
        /// </summary>
        public int BOMRev
        {
            get
            {
                return m_bomRev;
            }
            set
            {
                m_bomRev = value;
            }
        }
        private Int64 m_uploadedByID = 1;
        /// <summary>
        /// Database ID of the user who is uploading the BOM
        /// </summary>
        public Int64 UploadedByID
        {
            get
            {
                return m_uploadedByID;
            }
            set
            {
                m_uploadedByID = value;
            }
        }
        private int m_assyBUID;
        /// <summary>
        /// ID of the BU to which this product belongs
        /// </summary>
        public int AssyBUID
        {
            get
            {
                return m_assyBUID;
            }
            set
            {
                m_assyBUID = value;
            }
        }
        private string m_ReasonForRev;
        /// <summary>
        /// Reason user wants to upload BOM
        /// </summary>
        public string ReasonForRev
        {
            get
            {
                return m_ReasonForRev;
            }
            set
            {
                m_ReasonForRev = value;
            }
        }
        private Int64 m_AssyBOMID;
        /// <summary>
        /// Database ID in table asyBOM of this product revision
        /// </summary>
        public Int64 AssyBOMID
        {
            get
            {
                return m_AssyBOMID;
            }
            set
            {
                m_AssyBOMID = value;
            }
        }

        private Int64 m_keyReasonRev = 1;
        private Dictionary<string, Dictionary<string, AssyBomLineItem>> m_BOM;
        /// <summary>
        /// A dictionary fo dictionaries, containing the boms to upload:
        /// KEY=AssyPN ==> Dictionary, where each KEY=PN ==>AssyBOMLineItem object
        /// By iterating over the keys of the outer dictionary, we can upload BOMs for multiple Assys
        /// </summary>
        public Dictionary<string,Dictionary<string,AssyBomLineItem>> BOM
        {
            get
            {
                return m_BOM;
            }
        }

        private List<String> m_lstUndefinedPNs = new List<string>();
        /// <summary>
        /// List of PNs in the bom that are not defined in table otsParts
        /// </summary>
        public List<string> LstUndefinedPNs
        {
            get
            {
                return m_lstUndefinedPNs;
            }
        }

        private List<string> m_lstObsoleteParts = new List<string>();
        public List<string> LstObsoleteParts
        {
            get
            {
                return m_lstObsoleteParts;
            }
        }

        /// <summary>
        /// If all parts are OK, returns an empty string,
        /// otherwise returns a HTML list of all parts that were determined to obsolete/undefined when this object was
        /// constucted.
        /// </summary>
        public string HTML_BadPartMessage
        {
            get
            {
                string obsoleteParts = "";
                if (m_lstObsoleteParts.Count>0)
                {
                    m_lstObsoleteParts.Sort();
                    obsoleteParts = "<p>The following parts are obsolete:" +
                        DynControls.html_linebreak_string() +
                        string.Join(DynControls.html_linebreak_string(), m_lstObsoleteParts.ToArray()) +
                        "</p>";
                }

                string undefinedParts = "";
                if (m_lstUndefinedPNs.Count > 0)
                {
                    m_lstUndefinedPNs.Sort();
                    undefinedParts = "<p>The following parts are not defined in the database:" +
                        DynControls.html_linebreak_string() +
                        string.Join(DynControls.html_linebreak_string(), m_lstUndefinedPNs.ToArray()) +
                        "</p>";
                }
                if (obsoleteParts.Equals("") && undefinedParts.Equals(""))
                {
                    return "";
                } else
                {
                    return obsoleteParts + undefinedParts;
                }
            }
        }

        private StringBuilder m_errMsg = new StringBuilder();
        /// <summary>
        /// Returns any generated error messages in html format.
        /// </summary>
        public string HTML_ErrorMsg
        {
            get
            {
                if (m_errMsg.Length> 0) {
                    return "<p>" + m_errMsg.ToString().Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()) + "</p>";
                } else {
                    return "";
                }
                
            }
        }

        clsDB xDB = new clsDB();
        SqlCommand cmd = new SqlCommand();

        /// <summary>
        /// Creates a new Assy BOM Object;
        /// If successful, look at property BOM; if it fails, check property ErrorMsg
        /// Call method Upload to upload the object to the database
        /// </summary>
        /// <param name="topLevelName">aka the Product Name</param>
        /// <param name="assyPN">Part Number of the top level name; leave this blank, and the constructor will use
        /// the first AssyPN it encounters i the file specified in filePath as the Part Number for the assembly.</param>
        /// <param name="assyRev">Revision of the Assembly</param>
        /// <param name="uploaderID">ID of the user uploading the BOM</param>
        /// <param name="assyBU">BU of the BOM</param>
        /// <param name="bomRev">Revision of the Assembly BOM</param>
        /// <param name="reasonForRev">reason for the revision; OK to leave this blank, in which case
        /// the constructor will use the default key for Initial Release.</param>
        /// <param name="filePath">Path and file of the tsv file containing BOM information.</param>
        public clsAssyBOM(string topLevelName, string assyPN, string assyRev, int uploaderID, int assyBU,
            int bomRev, string reasonForRev, string filePath)
        {
            try
            {
                StringBuilder sB = new StringBuilder();

                m_topLevelName = topLevelName.ToUpper();
                m_assyPN = assyPN.ToUpper();
                m_assyRev = assyRev.ToUpper();
                m_uploadedByID = uploaderID;
                m_assyBUID = assyBU;
                m_bomRev = bomRev;
                m_ReasonForRev = reasonForRev;
                clsFileUtil f = new clsFileUtil(filePath);

                m_BOM = new Dictionary<string, Dictionary<string, AssyBomLineItem>>();
                using (StreamReader sR = f.OpenAndRead())
                {
                    string l = sR.ReadLine();   //First line is the header row; position of the headers tells us 
                                                //which columns to look at
                    int colAssyPN = -1;
                    int colRefDes = -1;
                    int colPN = -1;
                    int colQ = -1;
                    int colBOMNotes = -1;
                    int colInum = -1;
                    string[] arr = l.Split('\t');
                    for (int i = 0; i <= arr.GetUpperBound(0); i++)
                    {
                        switch (arr[i].ToLower())
                        {
                            case "bom item id":
                                colAssyPN = i;
                                break;
                            case "ref designator":
                                colRefDes = i;
                                break;
                            case "component id":
                                colPN = i;
                                break;
                            case "qty per assy":
                                colQ = i;
                                break;
                            case "bom notes":
                                colBOMNotes = i;
                                break;
                            case "i#":
                                colInum = i;
                                break;
                        }
                    }
                    //Loop the remainder of the file
                    while (!sR.EndOfStream)
                    {
                        l = sR.ReadLine();
                        arr = l.Split('\t');
                        string aPN = arr[colAssyPN];
                        if (!m_BOM.ContainsKey(aPN))
                        {
                            Dictionary<string, AssyBomLineItem> d = new Dictionary<string, AssyBomLineItem>();
                            m_BOM.Add(aPN, d);
                            if (m_assyPN == "")
                            {
                                m_assyPN = aPN;
                            }
                        }
                        m_BOM[aPN].Add(arr[colPN],
                            new AssyBomLineItem(arr[colPN], -1, arr[colRefDes].ToString().Replace(AAAK.DQ,""), arr[colBOMNotes], int.Parse(arr[colQ])));
                        sB.Append(arr[colPN] + ",");
                    }

                    string csvPN = sB.ToString().Substring(0, sB.Length - 1);

                    //Execute the stored procedure to:
                    //1) Determine if there are any invalid part numbers
                    //2) Get the ID for each PN
                    List<SqlParameter> ps = new List<SqlParameter>();
                    ps.Add(new SqlParameter("@csvPN", csvPN));
                    using (xDB.OpenConnection())
                    {
                        using (SqlDataReader dR = (SqlDataReader)xDB.ExecuteSP(DBK.SP.spOTSGETPNIDS, ps, clsDB.SPExMode.READER, ref cmd))
                        {
                            if (dR != null && dR.HasRows)
                            {
                                while (dR.Read())
                                {
                                    if (xDB.Fld2Str(dR[DBK.ID]) == "")
                                    {
                                        m_lstUndefinedPNs.Add(dR[DBK.SP_COLALIAS.PN].ToString());
                                    }
                                    else
                                    {
                                        foreach (string assyKy in m_BOM.Keys)
                                        {
                                            if (m_BOM[assyKy].ContainsKey(xDB.Fld2Str(dR[DBK.SP_COLALIAS.PN])))
                                            {
                                                m_BOM[assyKy][dR[DBK.SP_COLALIAS.PN].ToString()].PNID = Int64.Parse(dR[DBK.ID].ToString());
                                            }
                                        }

                                        if (int.Parse(dR[DBK.keyPARTSTATUS].ToString()) == 3)
                                        {
                                            m_lstObsoleteParts.Add(dR[DBK.SP_COLALIAS.PN].ToString());
                                        }
                                    }
                                }
                            }

                        }
                    }

                }
                f.Delete();
            } catch (Exception ex)
            {
                m_errMsg.Append("Error in clsAssyBOM.New:" + AAAK.vbCRLF + ex.Message + AAAK.vbCRLF + ex.StackTrace);
            }

        }


        public Boolean UploadToDB()
        {
            try
            {
                CustomCode u = new CustomCode();
                Int64 assyID = -1;
                if (m_lstObsoleteParts.Count>0 || m_lstUndefinedPNs.Count > 0)
                {
                    return false;
                }
                m_errMsg.Clear();
                //First, we need to make an entry in table asyBOM so we can get the DB ID of the Assy
                xDB = new clsDB();
                cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                string assyDesc = "ASSY," + m_topLevelName;
                ps.Add(new SqlParameter("@" + DBK.strNAME, m_topLevelName));
                ps.Add(new SqlParameter("@" + DBK.strASSYPARTNUMBER, m_assyPN));
                ps.Add(new SqlParameter("@" + DBK.strREVISION, m_assyRev));
                ps.Add(new SqlParameter("@" + DBK.intBOMREV, m_bomRev));
                ps.Add(new SqlParameter("@" + DBK.strDESCRIPTION, assyDesc));
                ps.Add(new SqlParameter("@intMajor", u.getMajorRev(m_assyRev)));
                ps.Add(new SqlParameter("@intMinor", u.getMinorRev(m_assyRev)));
                ps.Add(new SqlParameter("@" + DBK.keyUPLOADEDBY, u.getUserDBID()));
                ps.Add(new SqlParameter("@" + DBK.keyASSYBU, u.getUserdBUID()));
                ps.Add(new SqlParameter("@" + DBK.keyREASONFORREV, 1));
                ps.Add(new SqlParameter("@" + DBK.keyASSYSTATUS, 1));
                ps[ps.Count - 1].Direction = System.Data.ParameterDirection.InputOutput;
                ps.Add(new SqlParameter("@" + DBK.ID,-1));
                ps[ps.Count - 1].Direction = System.Data.ParameterDirection.Output;
                using (xDB.OpenConnection())
                {
                    xDB.ExecuteSP(DBK.SP.spUPSERTASSYBOMENTRY, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                    if (int.Parse(cmd.Parameters["@" + DBK.keyASSYSTATUS].Value.ToString()) == 1)
                    {
                        assyID = Int64.Parse(cmd.Parameters["@" + DBK.ID].Value.ToString());
                        //Delete this ASSY ID From the database
                        List<SqlParameter> tmpLstP = new List<SqlParameter>();
                        tmpLstP.Add(new SqlParameter("@" + DBK.keyASSY, assyID));
                        SqlCommand tmpCmd = new SqlCommand();
                        using (xDB.OpenConnection())
                        {
                            xDB.ExecuteSP(DBK.SP.spDELETEASSYBOMPARTS, tmpLstP, clsDB.SPExMode.NONQUERY, ref tmpCmd);
                        }
                        StringBuilder sqlStr = new StringBuilder();
                        sqlStr.Append("INSERT INTO " + DBK.asyBOMPARTS + " (" + DBK.keyASSY + ", " + DBK.keyPN + ", " +
                            DBK.strREFDES + ", " + DBK.strBOMNOTES + ", " + DBK.intQTY + ") VALUES ");
                        //The values we will insert are in m_BOM
                        foreach (string assyPNKy in m_BOM.Keys)
                        {
                            string abk = assyPNKy;
                            foreach (string PNKy in m_BOM[assyPNKy].Keys)
                            {
                                AssyBomLineItem x = m_BOM[assyPNKy][PNKy];
                                string bomNotes = x.BOMNotes;
                                if (bomNotes == "" || bomNotes == "-")
                                {
                                    bomNotes = "''";
                                }
                                sqlStr.Append("(" +
                                    assyID + ", " +
                                    x.PNID.ToString() + ", " +
                                    "'" + x.RefDes + "', " +
                                    bomNotes + ", " +
                                    x.Qty.ToString() + "),");
                            }
                        }

                        //Remove the last comma
                        sqlStr.Remove(sqlStr.Length - 1, 1);
                        cmd.Parameters.Clear();
                        using (xDB.OpenConnection())
                        {
                            if (xDB.ExecuteNonQuery(sqlStr.ToString()))
                            {
                                if (xDB.NAffectedRows > 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    m_errMsg.Append("Executed following query without errors:" +
                                    sqlStr + AAAK.vbCRLF + "...but no rows were affected by the Statement.  Please report this bug.");
                                    return false;
                                }
                            }
                            else
                            {
                                m_errMsg.Append("Method UploadToDB: Unable to execute Nonquery: " +
                                    sqlStr + AAAK.vbCRLF + xDB.ErrMsg);
                                return false;
                            }
                        }

                    }
                    else
                    {
                        m_errMsg.Append(m_topLevelName + " Revision " + m_assyRev + " BOM Revision " + m_bomRev +
                            "is RELEASED.  You cannot upload a new BOM if it's status is RELEASED." + AAAK.vbCRLF +
                            "If you want to upload a new BOM, you must change the Assembly and/or BOM revision.");
                        return false;
                    }
                }
 
            } catch (Exception ex)
            {
                m_errMsg.Append(ex.Message + AAAK.vbCRLF + ex.StackTrace);
                return false;
            }
        }

    }
}

public class AssyBomLineItem
{
    private Int64 m_PNID;
    /// <summary>
    /// Database ID (in table otsParts) of the Part Number
    /// </summary>
    public Int64 PNID
    {
        get
        {
            return m_PNID;
        }
        set
        {
            m_PNID = value;
        }
    }
    private string m_PN;
    /// <summary>
    /// Part Number of this line item
    /// </summary>
    public string PN
    {
        get
        {
            return m_PN;
        }
        set
        {
            m_PN = value;
        }
    }
    private string m_refDes;
    /// <summary>
    /// Reference Designators for this line item.
    /// </summary>
    public string RefDes
    {
        get
        {
            return m_refDes;
        }
        set
        {
            m_refDes = value;
        }
    }
    private string m_BomNotes;
    /// <summary>
    /// BOM Notes for this line item
    /// </summary>
    public string BOMNotes
    {
        get
        {
            return m_BomNotes;
        }
        set
        {
            m_BomNotes = value;
        }
    }
    private int m_qty;
    /// <summary>
    /// Qty of this line item
    /// </summary>
    public int Qty
    {
        get
        {
            return m_qty;
        }
        set
        {
            m_qty = value;
        }
    }

    public AssyBomLineItem(string PN, Int64 PNID, string rDes, string bNotes, int q)
    {
        m_PN = PN;
        m_PNID = PNID;
        m_refDes = rDes;
        m_BomNotes = bNotes;
        m_qty = q;
    }

}