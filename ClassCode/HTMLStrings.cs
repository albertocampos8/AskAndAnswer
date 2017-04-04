using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace AskAndAnswer.ClassCode
{
    public class HTMLStrings
    {
        public class ALIGN
        {
            public const string LEFT = "left";
            public const string RIGHT = "right";
            public const string CENTER = "center";
        }
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
            /// <param name="fill">Set TRUE if you want the contents of this cell to fill the entire cell (e.g., a button)</param>
            /// <param name="cntlID">If this is not equal to "", then the code will put a control in the cell using this ID.
            /// If the next two arguments are Null and "", then the control will be a text box.</param>
            /// <param name="dropDownOpts">If you supply a list, where 
            /// Even Items == Actual Value
            /// Odd Items == Displayed Value
            /// ...then a drop down box with that list will be placed in the table cell.</param>
            /// <param name="btnText">If this is not "", then we will put a button in the cell.</param>
            /// <param name="cntlClass">Allows you to assign a css class to the control enclosed by the cell, if available.</param>
            /// <param name="userInputEnabled">Set FALSE to disable the text area that is created by supplying an argument for
            /// textAreaID</param>
            /// <param name="toolTip">A tool tip that will show when user hovers over cell</param>
            /// <param name="selectionRequiredIfNoDefault">When TRUE, then if default value is -1, 'Selection Required'
            /// is shown (when default value of FALSE is used, first item in combobox is selected).  Usually this is set True when
            /// you are dealing with a null value in the database.</param>
            /// <param name="displayStyle">A valid css Display Style; This overrides property 'fill'.
            /// Example values: 'width:10px' or 'width:30px;height:40px' or 'display:none'</param>
            public TableCell(string id, string cssClass, string content, int span = 1, Boolean isHeader = false, 
                Boolean fill = false, string cntlID = "", List<string>dropDownOpts = null, string btnText = "", 
                string cntlClass = "", Boolean userInputEnabled = true, string toolTip = "",
                Boolean selectionRequiredIfNoDefault = false, string displayStyle = "")

            {
                m_id = id;
                m_cssClass = cssClass;
                m_contentString = content;
                m_span = span;
                m_isHeaderCell = isHeader;
                m_cntlID = cntlID;
                m_cntlCSSClass = cntlClass;
                m_lstOpts = dropDownOpts;
                m_btnText = btnText;
                m_UserInputEnabled = UserInputEnabled;
                m_toolTip = toolTip;
                m_selectionRequiredIfNoDefault = selectionRequiredIfNoDefault;
                m_displayStyle = displayStyle;
            }

            /// <summary>
            /// Renders the contents of this object as an html string for a table cell.
            /// </summary>
            /// <returns></returns>
            public string ToHTML()
            {
                try
                {
                    string cellContents = "";
                    string displayStyle = "";
                    string openTag = "<td ";
                    string closeTag = "</td>";
                    if (m_isHeaderCell)
                    {
                        openTag = "<th ";
                        closeTag = "</th>";
                    }
                    string propID = encodeProperty("id", m_id);
                    string propCssClass = encodeProperty("class", m_cssClass);
                    string propSpan = encodeProperty("colspan", m_span.ToString());
                    string tTip = encodeProperty("title", m_toolTip);
                    if (m_displayStyle!="")
                    {
                        displayStyle = encodeProperty("style", m_displayStyle);
                    }
                    else if (m_fill)
                    {
                        displayStyle = encodeProperty("style", "width:100%;height:100%");
                    }
                   
                    //What do we do with the content string?  Leave it as is or put it in a control?
                    if (m_cntlID != "")
                    {
                        if (m_lstOpts != null)
                        {
                            //Combobox
                            cellContents = DynControls.html_combobox_string(m_cntlID, m_lstOpts, m_cntlCSSClass, false,
                                m_contentString, true, AAAK.DISPLAYTYPES.BLOCK, "", !m_UserInputEnabled,
                                false, m_selectionRequiredIfNoDefault);
                        }
                        else if (m_btnText != "")
                        {
                            //button
                            cellContents = DynControls.html_button_string(m_cntlID, m_btnText, m_cntlCSSClass, true,
                                AAAK.DISPLAYTYPES.BLOCK, "", "", !m_UserInputEnabled);
                        }
                        else if (m_cntlID != "")
                        {
                            //if we have a control ID, but button text = "" and there is no list of options, we must have a textbox
                            cellContents = DynControls.html_txtbox_string(m_cntlID, m_cntlCSSClass, m_contentString, true,
                                AAAK.DISPLAYTYPES.BLOCK, "", !m_UserInputEnabled);
                        }

                    } else
                    {
                        //as is
                        cellContents = m_contentString;
                    }

                    return openTag + propID + propCssClass + propSpan + tTip + displayStyle + ">" + cellContents + closeTag;

                } catch (Exception ex)
                {
                    return "<td>" + ex.ToString() + "</td>";
                }
            }

            private Boolean m_fill = false;
            public Boolean Fill
            {
                get
                {
                    return m_fill;
                }
                set
                {
                    m_fill = value;
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

            private string m_cntlID = "";
            /// <summary>
            /// Set TRUE to put a text area in this cell
            /// </summary>
            public string ControlID
            {
                get
                {
                    return m_cntlID;
                }
                set
                {
                    m_cntlID = value;
                }
            }

            private string m_toolTip = "";
            public string ToolTip
            {
                get
                {
                    return m_toolTip;
                } 
                set
                {
                    m_toolTip = value;
                }
            }

            private string m_cntlCSSClass = "";
            /// <summary>
            /// Set TRUE to put a text area in this cell
            /// </summary>
            public string ControlCSSClass
            {
                get
                {
                    return m_cntlCSSClass;
                }
                set
                {
                    m_cntlCSSClass  = value;
                }
            }

            private string m_btnText = "";
            public string ButtonText
            {
                get
                {
                    return m_btnText;
                }
                set
                {
                    m_btnText = value;
                }
            }

            private List<string> m_lstOpts = null;
           public List<string>ListOfOptions
            {
                get
                {
                    return m_lstOpts;
                }
                set
                {
                    m_lstOpts = value;
                }
            }
            private Boolean m_UserInputEnabled = false;
            /// <summary>
            /// Set FALSE to disable user input, TRUE to enable
            /// </summary>
            public Boolean UserInputEnabled
            {
                get
                {
                    return m_UserInputEnabled;
                }
                set
                {
                    m_UserInputEnabled = value;
                }
            }
            string m_id = "";
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

            private Boolean m_selectionRequiredIfNoDefault = false;
            private string m_displayStyle = "";

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
                    string display = DynControls.DecodeDisplayValue(m_displayStyle);
                    sB.Append(openTag + propID + propCssClass + display + " >");
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
            private AAAK.DISPLAYTYPES m_displayStyle;
            public AAAK.DISPLAYTYPES DisplayStyle
            {
                get
                {
                    return m_displayStyle;
                }
                set
                {
                    m_displayStyle = value;
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
                    string width = "";
                    sB.Append(openTag + propID + propCssClass + width + " >");
                    for (int i = 0; i < m_Rows.Count; i++)
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

        /// <summary>
        /// Returns stylized text for a heading:
        /// First Character is Size Medium, all others are Small.
        /// All letters are capitalized
        /// </summary>
        /// <param name="headingText">The heading text</param>
        /// <param name="headingLevel">Heading level, e.g., h1, h2, etc.</param>
        /// <param name="cssclass">Optional css class</param>
        /// <returns></returns>
        public static string HTMLHeadingStyle1(string headingText, string headingLevel, string cssclass = "", string id = "")
        {
            string qClass = "";
            string qID = "";
            qClass = DynControls.encodeProperty("class", cssclass);
            qID = DynControls.encodeProperty("class", id);
            string start = headingText.Substring(0, 1).ToUpper();
            string rest = headingText.Substring(1, headingText.Length - 1).ToUpper();
            return "<" + headingLevel + " " + qClass + qID + 
                "<span " + DynControls.encodeProperty("style", "font-size: medium") + ">" +
                start + "</span>" +
                "<span " + DynControls.encodeProperty("style", "font-size: small") + ">" +
                rest + "</span></h2>";
        }

        public static string HTMLHeading(string headingText, string headingLevel, string align = "",
           string cssClass = "")
        {
            try
            {
                string qAlign = DynControls.encodeProperty("align", align);
                string qClass = DynControls.encodeProperty("class", cssClass);
                return "<" + headingLevel + " " + qClass + qAlign + ">" + headingText + "</" + headingLevel + ">";
            } catch (Exception ex)
            {
                return "<p>Error in HTMLStrings.HTMLHeading: " + ex.Message + "</p>";
            }
        }
    }

}