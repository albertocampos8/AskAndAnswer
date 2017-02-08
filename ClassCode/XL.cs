using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;    //For xlsx (EEPlus)
using System.IO;
using NPOI.HSSF.UserModel;  //For .xls; supposed works with xlsx file too???
using NPOI.SS.UserModel;

namespace AskAndAnswer.ClassCode
{
    public class XL
    {
        public enum xType {none, xls, xlsx};

        private object m_app = null;
        private object m_wkbk = null;
        private object m_wksht = null;
        private xType m_type = xType.none;
        private string m_errMsg = "";
        public string ErrMsg
        {
            get
            {
                return m_errMsg;
            }
        }

        private int m_firstRow = 0;
                /// <summary>
        /// Index of first row in file.
        /// </summary>
        public int FirstRow
        {
            get { return m_firstRow; }
        }

        public int m_firstCol = 0;

        /// <summary>
        /// Index of first col in file
        /// </summary>
        public int FirstCol
        {
            get { return m_firstCol; }
        }

        /// <summary>
        /// Instantiator; sets the m_app and m_wkbk object properties based on the path and file
        /// </summary>
        /// <param name="pathAndFile">Full path and and name of file to read/create</param>
        /// <param name="overwrite">When TRUE, file will be deleted if it exists.  Leave this False if you are reading a file!</param>
        public XL(string pathAndFile, Boolean overwrite = false)
        {
            try
            {
                string extension = pathAndFile.Split('.')[pathAndFile.Split('.').GetUpperBound(0)].ToLower();
                switch (extension)
                {
                    case "xls":
                        m_type = xType.xls;
                        m_firstRow = 0;
                        m_firstCol = 0;
                        break;
                    case "xlsx":
                        m_firstRow = 1;
                        m_firstCol = 1;
                        m_type = xType.xlsx;
                        break;
                    default:
                        m_errMsg = "Invalid File Name: You must provide a file name that ends in either .xls or .xlsx.";
                        return;
                }
                FileInfo f = new FileInfo(@pathAndFile);
                if (f.Exists && overwrite)
                {
                    f.Delete();
                    f = new FileInfo(pathAndFile);
                }

                switch (m_type)
                {
                    case xType.xls:
                        using (FileStream fstream = new FileStream(f.FullName ,FileMode.Open, FileAccess.Read))
                        {
                            //Note NPOI has no 'application' equivalent object, so m_app stays null.
                            //We start directly from a workbook object
                            HSSFWorkbook wb = new HSSFWorkbook(fstream);
                            m_wkbk = wb;
                        }
                        break;
                    case xType.xlsx:
                        ExcelPackage xlsx = new ExcelPackage(f);
                        m_app = xlsx;
                        m_wkbk = ((ExcelPackage)m_app).Workbook;
                        break;
                    default:
                        m_app = null;
                        break;
                }
            } catch (Exception ex)
            {
                m_errMsg = ex.Message + AAAK.vbCRLF + ex.StackTrace;
            }
        }

        /// <summary>
        /// Sets the worksheet in the workbook object to sheetName.
        /// </summary>
        /// <param name="sheetName">Name of the sheet we want to activate; set this to empty string to simply get the sheet
        /// at index 0</param>
        public void SetWorkSheet(string sheetName)
        {
            try
            {
                switch (m_type)
                {
                    case xType.xls:
                        if (sheetName == "")
                        {
                            m_wksht = ((HSSFWorkbook)m_wkbk).GetSheetAt(0);
                        }
                        else
                        {
                            m_wksht = ((HSSFWorkbook)m_wkbk).GetSheet(sheetName);
                        }
                        break;
                    case xType.xlsx:
                        if (sheetName=="")
                        {
                            m_wksht = ((ExcelWorkbook)m_wkbk).Worksheets[1];
                        } else
                        {
                            m_wksht = ((ExcelWorkbook)m_wkbk).Worksheets[sheetName];
                        }
                        
                        break;
                    default:
                        m_wksht = null;
                        break;
                }
            } catch (Exception ex)
            {
                m_errMsg = ex.Message + AAAK.vbCRLF + ex.StackTrace;
            }
        }

        /// <summary>
        /// Returns true if sheetName exists in m_wkbk
        /// </summary>
        /// <param name="sheetName">Name of sheet we are looking for</param>
        /// <returns></returns>
        public Boolean SheetExists(string sheetName)
        {
            try
            {
                switch(m_type)
                {
                    case xType.xls:

                        break;
                    case xType.xlsx:
                        m_wksht = ((ExcelWorkbook)m_wkbk).Worksheets[sheetName];
                        break;
                    default:
                        m_wksht = null;
                        break;
                }
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets value at cell (r,c) of current Worksheet Object
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetCellValue(int r, int c)
        {
            try
            {
                switch (m_type)
                {
                    case xType.xls:
                        if (((ISheet)m_wksht).GetRow(r).GetCell(c) == null)
                        {
                            return "";
                        }
                        else
                        {
                            return ((ISheet)m_wksht).GetRow(r).GetCell(c).StringCellValue;
                        }
                    case xType.xlsx:
                        if (((ExcelWorksheet)m_wksht).Cells[r, c].Value == null)
                        {
                            return "";
                        } else
                        {
                            return ((ExcelWorksheet)m_wksht).Cells[r, c].Value.ToString();
                        }
                       
                    default:
                        return "GetCell Value ERROR - TYPE UNDEFIED";
                }
            }
            catch (Exception ex)
            {
                m_errMsg = "GetCell Value ERROR - " + ex.Message + AAAK.vbCRLF + ex.StackTrace;
                return "";
            }
        }

        /// <summary>
        /// Sets value at cell (r,c) of current Worksheet Object to value; returns True if successful
        /// </summary>
        /// <param name="r">Row Index</param>
        /// <param name="c">Col Index</param>
        /// <param name="value">Value to put in cell; this is an object, so can pass string or integer (this makes a difference
        /// in how Excel will treat the value when formatting)</param>
        /// <returns></returns>
        public Boolean SetCellValue(int r, int c, object value)
        {
            try
            {
                switch (m_type)
                {
                    case xType.xls:
                        if (value is double)
                        {
                            ((ISheet)m_wksht).GetRow(r).GetCell(c).SetCellValue((double)value);
                        }
                        else if (value is Boolean)
                        {
                            ((ISheet)m_wksht).GetRow(r).GetCell(c).SetCellValue((Boolean)value);
                        }
                        else if (value is DateTime)
                        {
                            ((ISheet)m_wksht).GetRow(r).GetCell(c).SetCellValue((DateTime)value);
                        }
                        else if (value is IRichTextString)
                        {
                            ((ISheet)m_wksht).GetRow(r).GetCell(c).SetCellValue((IRichTextString)value);
                        }
                        else
                        {
                            ((ISheet)m_wksht).GetRow(r).GetCell(c).SetCellValue((string)value);
                        }
                        return true;
                    case xType.xlsx:
                        ((ExcelWorksheet)m_wksht).Cells[r, c].Value = value;
                        return true;
                    default:
                        m_errMsg = "SetCell Value ERROR - TYPE UNDEFIED";
                        return false;
                }
            }
            catch (Exception ex)
            {
                m_errMsg = ex.Message + AAAK.vbCRLF + ex.StackTrace;
                return false;
            }
        }

        public Boolean SaveWorkbook(string pathAndFileName)
        {
            try
            {
                FileInfo f = new FileInfo(pathAndFileName);
                if (f.Exists)
                {
                    //We are saving an existing file
                    switch (m_type)
                    {
                        case xType.xls:
                            //Apparently, NPOI does not have a save method for the workbook.
                            //Following online examples:
                            using (FileStream outFile = new FileStream(pathAndFileName, FileMode.Create, FileAccess.Write))
                            {
                                ((HSSFWorkbook)m_wkbk).Write(outFile);
                            }
                            return true;
                        case xType.xlsx:
                            ((ExcelPackage)m_app).Save();
                            return true;
                        default:
                            m_errMsg = "SaveWorkbook ERROR - TYPE UNDEFIED";
                            return false;
                    }
                } else
                {
                    //The file does not yet exist; we must use save as
                    switch (m_type)
                    {
                        case xType.xls:
                            //Since FileMode.Create makes a new file or overwrites an existing one, we can use the same approach
                            using (FileStream outFile = new FileStream(pathAndFileName, FileMode.Create, FileAccess.Write))
                            {
                                ((HSSFWorkbook)m_wkbk).Write(outFile);
                            }
                            return true;
                        case xType.xlsx:
                            ((ExcelPackage)m_app).SaveAs(f);
                            return true;
                        default:
                            m_errMsg = "SetCell Value ERROR - TYPE UNDEFIED";
                            return false;
                    }

                }

            }
            catch (Exception ex)
            {
                m_errMsg = ex.Message + AAAK.vbCRLF + ex.StackTrace;
                return false;
            }
        }

        public Boolean CloseDispose()
        {
            try
            {
                    switch (m_type)
                    {
                        case xType.xls:
                            try
                            {
                                ((HSSFWorkbook)m_wkbk).Close();
                            } catch (Exception ex)
                            {
                                string x = ex.Message + ex.StackTrace;
                            }
                            return true;
                        case xType.xlsx:
                            try
                            {
                                ((ExcelWorksheet)m_wksht).Dispose();
                            } catch (Exception ex)
                            {
                                string x = ex.Message + ex.StackTrace;
                            }
                            try
                            {
                                ((ExcelWorkbook)m_wkbk).Dispose();
                            }
                            catch (Exception ex)
                            {
                                string x = ex.Message + ex.StackTrace;
                            }
                            try
                            {
                                ((ExcelPackage)m_app).Dispose();
                            }
                            catch (Exception ex)
                            {
                                string x = ex.Message + ex.StackTrace;
                            }
                            return true;
                        default:
                            m_errMsg = "Close Dispose - TYPE UNDEFIED";
                            return false;
                    }
            }
            catch (Exception ex)
            {
                m_errMsg = ex.Message + AAAK.vbCRLF + ex.StackTrace;
                return false;
            }

        }

    }
}