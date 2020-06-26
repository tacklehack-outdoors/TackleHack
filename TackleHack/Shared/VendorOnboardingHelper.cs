using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Linq;
using TackleHack.Models;

namespace TackleHack.Shared
{
    public class VendorOnboardingHelper
    {
        public static void ProcessVendorProducts(int vendorId, String filePath)
        {
            using (var context = new TackleHackSQLContext())
            {
                DataTable vendorData = ReadFileToDataTable(filePath);
                var columnNames = vendorData.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
                var featureColumns = columnNames.Where(x => x.Contains("Key_Feature"));
                foreach(DataRow row in vendorData.Rows)
                {
                    var product = new Product()
                    {
                        BrandName = (String) row["BRAND"],
                        ItemNumber = (String) row["SKU"],
                        Sku = (String) row["SKU"],
                        ProductName = (String) row["Product Name"],
                        Description = (String) row["Product Description"],
                        Msrp = (double) row["Price"],
                        VendorId = vendorId
                    };

                    context.Product.Add(product);
                    context.SaveChanges();

                    foreach (String column in featureColumns)
                    {
                        var description = (row[column] == DBNull.Value ? string.Empty : (String) row[column]);
                        if (!String.IsNullOrEmpty(description))
                        {
                            var feature = context.Feature.Where(x => x.Description.Equals(description)).FirstOrDefault();
                            if (feature == null)
                            {
                                feature = new Feature()
                                {
                                    Description = description
                                };
                                context.Feature.Add(feature);
                                context.SaveChanges();
                            }

                            var productFeature = new ProductFeature()
                            {
                                ProductId = product.Id,
                                FeatureId = feature.Id
                            };
                            context.ProductFeature.Add(productFeature);
                            context.SaveChanges();
                        }
                    }

                    var videoLink = (row["Video"] == DBNull.Value ? string.Empty : (String) row["Video"]);
                    if (!String.IsNullOrEmpty(videoLink))
                    {
                        var media = new Media()
                        {
                            Title = product.ProductName,
                            Link = videoLink,
                            ProductId = product.Id
                        };
                        context.Media.Add(media);
                        context.SaveChanges();
                    }
                }
            }
        }

        public static DataTable ReadFileToDataTable(String filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var dataTable = dataSet.Tables[0];
                    return dataTable;
                }
            }
        }
    }
}
