using ClosedXML.Excel;
using System.Data;
using System.Reflection;

namespace UzEx.Analytics.Application.Extensions;

public static class ExcelExporter
{
    public static Task<byte[]> ExportToExcel<T>(List<T> data, string sheetName) where T : class
    {
        var dataTable = ToDataTable(data);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(dataTable, sheetName);


        // Columns already auto-fit to contents in table
        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return Task.FromResult(stream.ToArray());
    }

    private static DataTable ToDataTable<T>(IList<T> data)
    {
        var dataTable = new DataTable(typeof(T).Name);

        // Get all public instance properties
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Create columns
        foreach (var prop in properties)
        {
            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            dataTable.Columns.Add(prop.Name, propType);
        }

        // Add rows
        foreach (var item in data)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item) ?? DBNull.Value;
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }
}
