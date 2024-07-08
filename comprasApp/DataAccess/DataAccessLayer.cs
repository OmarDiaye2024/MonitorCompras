using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace comprasApp.DataAccess
{
    public class DataAccessLayer
    {
        private string connectionString = "Data Source=(local);Initial Catalog=compras;Integrated Security=True;Connect Timeout=900"; // Reemplaza con tu cadena de conexión


        public List<SelectListItem> getSelectItems(string query, string textColumn, string valueColumn)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = reader[textColumn].ToString(),
                                Value = reader[valueColumn].ToString()
                            });
                        }
                    }
                }
            }

            return items;
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 900;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
        public void ExportToExcel(string query, string filePath)
        {
            try
            {
                DataTable dataTable = ExecuteQuery(query);

                // Crear un nuevo libro de Excel
                using (var workbook = new XLWorkbook())
                {
                    // Añadir una hoja al libro
                    var worksheet = workbook.Worksheets.Add("Resultados");

                    // Agregar los encabezados de las columnas
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                    }

                    // Agregar los datos
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            //worksheet.Cell(i + 2, j + 1).Value = dataTable.Rows[i][j].ToString();
                            object cellValue = dataTable.Rows[i][j];
                            var cell = worksheet.Cell(i + 2, j + 1);

                            if (int.TryParse(cellValue.ToString(), out int intValue))
                            {
                                // La conversión a int fue exitosa
                                cell.Value = intValue;
                                cell.Style.NumberFormat.Format = "0"; // Formato de número entero
                            }
                            else if (double.TryParse(cellValue.ToString(), out double doubleValue))
                            {
                                // La conversión a double fue exitosa
                                cell.Value = doubleValue;
                                cell.Style.NumberFormat.Format = "0.00"; // Ajusta el formato según sea necesario
                            }
                            else
                            {
                                // Mantener como texto
                                cell.Value = cellValue.ToString();
                            }
                        }
                    }
                    // Guardar el libro de Excel
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }

        public void ExportToExcelSheets(string query, string filePath)
        {
            try
            {
                DataTable dataTable = ExecuteQuery(query);

                using (var workbook = new XLWorkbook())
                {
                    int maxRowsPerSheet = 1000000; // Límite máximo de filas en una hoja de Excel (para formato XLSX)

                    for (int sheetIndex = 0; sheetIndex < Math.Ceiling((double)dataTable.Rows.Count / maxRowsPerSheet); sheetIndex++)
                    {
                        var worksheet = workbook.Worksheets.Add("Resultados" + (sheetIndex + 1));

                        // Agregar los encabezados de las columnas
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                        }

                        int startRow = sheetIndex * maxRowsPerSheet;

                        for (int i = startRow; i < Math.Min(startRow + maxRowsPerSheet, dataTable.Rows.Count); i++)
                        {
                            for (int j = 0; j < dataTable.Columns.Count; j++)
                            {
                                object cellValue = dataTable.Rows[i][j];
                                var cell = worksheet.Cell(i - startRow + 2, j + 1);

                                if (int.TryParse(cellValue.ToString(), out int intValue))
                                {
                                    cell.Value = intValue;
                                    cell.Style.NumberFormat.Format = "0";
                                }
                                else if (double.TryParse(cellValue.ToString(), out double doubleValue))
                                {
                                    cell.Value = doubleValue;
                                    cell.Style.NumberFormat.Format = "0";
                                }
                                else
                                {
                                    cell.Value = cellValue.ToString();
                                }
                            }
                        }
                    }

                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ExportToExcelInBlock(string query, string filePath)
        {
            try
            {
                DataTable dataTable = ExecuteQuery(query);

                using (var workbook = new XLWorkbook())
                {
                    int maxRowsPerSheet = 1000000; // Límite máximo de filas en una hoja de Excel (para formato XLSX)
                    int totalRows = dataTable.Rows.Count;

                    for (int sheetIndex = 0; sheetIndex * maxRowsPerSheet < totalRows; sheetIndex++)
                    {
                        var worksheet = workbook.Worksheets.Add("Resultados" + (sheetIndex + 1));

                        // Obtener las filas para esta hoja
                        var rows = dataTable.AsEnumerable()
                            .Skip(sheetIndex * maxRowsPerSheet)
                            .Take(maxRowsPerSheet)
                            .CopyToDataTable(); // Convertir DataView a DataTable

                        // Cargar los datos desde el DataTable en la hoja de Excel
                        worksheet.Cell(1, 1).InsertTable(rows, "Resultados", true);
                    }

                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //public byte[] ExportToExcel(string query)
        //{
        //    DataTable dataTable = ExecuteQuery(query);
        //    // Crear un nuevo libro de Excel
        //    using (var workbook = new XLWorkbook())
        //    {
        //        // Añadir una hoja al libro
        //        var worksheet = workbook.Worksheets.Add("Resultados");

        //        // Agregar los encabezados de las columnas
        //        for (int i = 0; i < dataTable.Columns.Count; i++)
        //        {
        //            worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
        //        }

        //        // Agregar los datos
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dataTable.Columns.Count; j++)
        //            {
        //                object cellValue = dataTable.Rows[i][j];
        //                var cell = worksheet.Cell(i + 2, j + 1);

        //                if (cellValue is int || cellValue is long)
        //                {
        //                    // Formatear como número entero
        //                    cell.Value = Convert.ToInt64(cellValue);
        //                    cell.Style.NumberFormat.NumberFormatId = 1; // Formato de número entero
        //                }
        //                else if (cellValue is double || cellValue is float || cellValue is decimal)
        //                {
        //                    // Formatear como número con decimales
        //                    cell.Value = Convert.ToDouble(cellValue);
        //                    cell.Style.NumberFormat.Format = "0.00"; // Ajusta el formato según sea necesario
        //                }
        //                else
        //                {
        //                    // Mantener como texto para otros tipos de datos
        //                    cell.Value = cellValue.ToString();
        //                }
        //            }
        //        }

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            return stream.ToArray();
        //        }
        //    }
        //}

    }
}