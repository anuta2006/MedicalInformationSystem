using MedicalInformationSystem.Foundation.MediaData;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;

namespace MedicalInformationSystem.Foundation.Reports.Word
{
    public static class WordDocument
    {
        //Create document method
        public static void CreateDocument()
        {
            try
            {
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //Add header into the document
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                    headerRange.Font.Size = 14;
                    headerRange.Text = "Обращения за период";
                }

                //Add the footers into the document
                //foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                //{
                //    //Get the footer range and add the footer details.
                //    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                //    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                //    footerRange.Font.Size = 10;
                //    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                //    footerRange.Text = "Footer text goes here";
                //}

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = "This is test document " + Environment.NewLine;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                //object styleHeading1 = "Heading 1";
               // para1.Range.set_Style(ref styleHeading1);
                //para1.Range.Text = "Para 1 text";
                //para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
               // Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
               // object styleHeading2 = "Heading 2";
                //para2.Range.set_Style(ref styleHeading2);
              //  para2.Range.Text = "Para 2 text";
                //para2.Range.InsertParagraphAfter();

                //Create a 5X5 table and insert some dummy record
                Table firstTable = document.Tables.Add(para1.Range, 5, 9, ref missing, ref missing);

                var reportsByPeriod = new ReportInfo[]
            {
                new ReportInfo()
                {
                    Class = "5A",
                    Date = "20.05.2017",
                    Diagnosis = "ОРВИ",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "температура тела 37,8"
                },
                new ReportInfo()
                {
                    Class = "2А",
                    Date = "18.05.2017",
                    Diagnosis = "Растяжение связок",
                    FirstName = "Илья",
                    LastName = "Сидоров",
                    Patronymic = "Николаевич",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в руке"
                },
                new ReportInfo()
                {
                    Class = "1A",
                    Date = "18.05.2017",
                    Diagnosis = "Пищевое отравление",
                    FirstName = "Екатерина",
                    LastName = "Драенкова",
                    Patronymic = "Витальевна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "тошнота, температура тела 37,8"
                },
                new ReportInfo()
                {
                    Class = "9Б",
                    Date = "17.05.2017",
                    Diagnosis = "Язвенная болезнь",
                    FirstName = "Анастасия",
                    LastName = "Хацкевич",
                    Patronymic = "Леонидовна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "болезнь в желудке"
                },
                new ReportInfo()
                {
                    Class = "4В",
                    Date = "17.05.2017",
                    Diagnosis = "Ушиб",
                    FirstName = "Марина",
                    LastName = "Селиванов",
                    Patronymic = "Вадимовна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "повреждена нога"
                },
                new ReportInfo()
                {
                    Class = "10A",
                    Date = "15.05.2017",
                    Diagnosis = "Отит",
                    FirstName = "Светлана",
                    LastName = "Мороз",
                    Patronymic = "Евгеньевна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в ухе"
                },
            };

                firstTable.Borders.Enable = 1;
                foreach (Row row in firstTable.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        //Header row
                        if (cell.RowIndex == 1)
                        {
                            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                            cell.Range.Font.Bold = 1;
                            //other format properties goes here
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;
                            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            //Center alignment for the Header cells
                            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                        }
                        //Data row
                        else
                        {
                            //var temp = reportsByPeriod[cell.ColumnIndex];
                            //cell.Range.Text = temp.
                        }
                    }
                }

                //Save the document
                object filename = @"D:\Отчет.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
              //  MessageBox.Show("Document created successfully !");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
