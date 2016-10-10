using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace AskAndAnswer.ClassCode
{
    public class HTMLStrings
    {
        /// <summary>
        /// A class used to generate the html for a cell in an html table
        /// </summary>
        public class TableCell
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="id">The id for the table cell</param>
            /// <param name="cssClass">The CSS Class for the table cell</param>
            /// <param name="content">Content of the table cell.  Can be text or html-formatted text</param>
            /// <param name="span">Number of columns this cell occupies; default = 1</param>
            /// <param name="isHeader">Set TRUE if this is a header cell; default is false</param>
            public TableCell(string id, string cssClass, string content, int span = 1, Boolean isHeader = false)
            {
                m_id = id;
                m_cssClass = cssClass;
                m_contentString = content;
                m_span = span;
                m_isHeaderCell = isHeader;
            }

            /// <summary>
            /// Renders the contents of this object as an html string for a table cell.
            /// </summary>
            /// <returns></returns>
            public string ToHTML()
            {
                try
                {
                    string openTag = "<td ";
                    string closeTag = "</td>";
                    if (m_isHeaderCell)
                    {
                        openTag = "<th ";
                        closeTag = "</th>";
                    }
                    string propID = encodeProperty("id", m_id);
                    string propCssClass = encodeProperty("class", m_cssClass);
                    string propSpan = encodeProperty("span", m_span.ToString());
                    return openTag + propID + propCssClass + propSpan + ">" + ContentString + closeTag;

                } catch (Exception ex)
                {
                    return "<td>" + ex.ToString() + "</td>";
                }
            }


            private Boolean m_isHeaderCell = false;
            /// <summary>
            /// True if this is a header cell <th>, false if data cell <td>
            /// </summary>
            public Boolean IsHeaderCell
            {
                get
                {
                    return m_isHeaderCell;
                }
                set
                {
                    m_isHeaderCell = value;
                }
            }

            private string m_id = "";
            /// <summary>
            /// The ID used for this cell
            /// </summary>
            public string ID
            {
                get
                {
                    return m_id;
                }
                set
                {
                    m_id = value;
                }
            }

            private string m_cssClass = "";
            /// <summary>
            /// The CSS Class for this cell
            /// </summary>
            public string CssClass
            {
                get
                {
                    return m_cssClass;
                }
                set
                {
                    m_cssClass = value;
                }
            }
            
            private int m_span = 1;
            /// <summary>
            /// The number of columns this cell spans
            /// </summary>
            public int Span
            {
                get
                {
                    return m_span;
                }
                set
                {
                    m_span = value;
                }
            }

            private string m_contentString = "";
            /// <summary>
            /// The string that appears within the <td> or <th> tags.
            /// This can be normal text or text formatted with html.
            /// </summary>
            public string ContentString
            {
                get
                {
                    return m_contentString;
                }
                set
                {
                    m_contentString = value;
                }
            }

        }

        /// <summary>
        /// A class used to generate the html for a row in an html table
        /// </summary>
        public class TableRow
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="id">The id for the table row</param>
            /// <param name="cssClass">The CSS Class for the table row</param>
            /// <param name="cells">Array of TableCell objects contained in this row.  Element 0 is the left-most cell.</param>
            public TableRow(string id, string cssClass, TableCell[] cells)
            {
                m_id = id;
                m_cssClass = cssClass;
                m_Cells = cells;
            }

            /// <summary>
            /// Renders the contents of this object as an html string for a table row.
            /// </summary>
            /// <returns></returns>
            public string ToHTML()
            {
                try
                {
                    StringBuilder sB = new StringBuilder();
                    string openTag = "<tr ";
                    string closeTag = "</tr>";
                    string propID = encodeProperty("id", m_id);
                    string propCssClass = encodeProperty("class", m_cssClass);
                    sB.Append(openTag + propID + propCssClass + " >");
                    for (int i = 0; i< m_Cells.Length;i++)
                    {
                        sB.Append(m_Cells[i].ToHTML());
                    }
                    sB.Append(closeTag);
                    return sB.ToString();

                }
                catch (Exception ex)
                {
                    return "<td>" + ex.ToString() + "</td>";
                }
            }

            private string m_id = "";
            /// <summary>
            /// The ID used for this row
            /// </summary>
            public string ID
            {
                get
                {
                    return m_id;
                }
                set
                {
                    m_id = value;
                }
            }

            private string m_cssClass = "";
            /// <summary>
            /// The CSS Class for this row
            /// </summary>
            public string CssClass
            {
                get
                {
                    return m_cssClass;
                }
                set
                {
                    m_cssClass = value;
                }
            }

            private TableCell[] m_Cells;
            public TableCell[] Cells
            {
                get
                {
                    return m_Cells;
                }
                set
                {
                    m_Cells = value;
                }
            }


        }

        /// <summary>
        /// A class that generates the html code for a table
        /// </summary>
        public class Table
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="id">The id for the table</param>
            /// <param name="cssClass">The CSS Class for the table</param>
            /// <param name="cells">List of TableRow objects contained in this table.  Element 0 is the top-most row.</param>
            public Table(string id, string cssClass, List<TableRow> rows)
            {
                m_id = id;
                m_cssClass = cssClass;
                m_Rows = rows;
            }

            /// <summary>
            /// Renders the contents of this object as an html string for a table row.
            /// </summary>
            /// <returns></returns>
            public string ToHTML()
            {
                try
                {
                    StringBuilder sB = new StringBuilder();
                    string openTag = "<table ";
                    string closeTag = "</table>";
                    string propID = encodeProperty("id", m_id);
                    string propCssClass = encodeProperty("class", m_cssClass);
                    sB.Append(openTag + propID + propCssClass + " >");
                    for (int i = 0; i < m_Rows.Count - 1; i++)
                    {
                        sB.Append(m_Rows[i].ToHTML());
                    }
                    sB.Append(closeTag);
                    return sB.ToString();

                }
                catch (Exception ex)
                {
                    return "<td>" + ex.ToString() + "</td>";
                }
            }

            private string m_id = "";
            /// <summary>
            /// The ID used for this row
            /// </summary>
            public string ID
            {
                get
                {
                    return m_id;
                }
                set
                {
                    m_id = value;
                }
            }

            private string m_cssClass = "";
            /// <summary>
            /// The CSS Class for this row
            /// </summary>
            public string CssClass
            {
                get
                {
                    return m_cssClass;
                }
                set
                {
                    m_cssClass = value;
                }
            }

            private List<TableRow> m_Rows;
            public List<TableRow> Rows
            {
                get
                {
                    return m_Rows;
                }
                set
                {
                    m_Rows = value;
                }
            }


        }

        /// <summary>
        /// Puts double quotes around the value of an html property
        /// </summary>
        /// <param name="propName">Propety Name</param>
        /// <param name="propValue">Property Value</param>
        /// <returns></returns>
        private static String encodeProperty(string propName, string propValue)
        {
            if (propValue == "")
            {
                return "";
            }
            else
            {
                return propName + "=" + AAAK.DQ + propValue + AAAK.DQ + " ";
            }
        }
    }

}