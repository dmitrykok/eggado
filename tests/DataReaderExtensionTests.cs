﻿namespace Eggado.Tests
{
    #region Imports

    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Xml;
    using Mannex.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    [TestClass]
    public class DataReaderExtensionTests
    {
        class Product
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string EnglishName { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
            public int UnitsOnOrder { get; set; }
            public int? ReorderLevel { get; set; }
            public bool Discontinued { get; set; }
            public string Supplier { get; set; }
            public string Category { get; set; }
        }

        private IDataReader GetProducts()
        {
            var table = new DataTable();
            using (var reader = GetType().Assembly.GetManifestResourceReader(GetType(), "Products.xml", Encoding.UTF8))
            {
                Assert.IsNotNull(reader);
                table.ReadXml(XmlReader.Create(reader));
            }
            return new DataTableReader(table);
        }

        [TestMethod]
        public void Select()
        {
            AssertProducts(GetProducts().Select<Product>());
        }
        
        [TestMethod]
        public void SelectViaSelector()
        {
            var products = GetProducts().Select(
            (
                int productId, string productName, string englishName, 
                string quantityPerUnit, decimal unitPrice, 
                int unitsInStock, int unitsOnOrder, int? reorderLevel, 
                bool discontinued, string supplier, string category
            ) 
            => new Product
            {
                ProductId       = productId,
                ProductName     = productName,
                EnglishName     = englishName,
                QuantityPerUnit = quantityPerUnit,
                UnitPrice       = unitPrice,
                UnitsInStock    = unitsInStock,
                UnitsOnOrder    = unitsOnOrder,
                ReorderLevel    = reorderLevel,
                Discontinued    = discontinued,
                Supplier        = supplier,
                Category        = category,
            });

            AssertProducts(products);
        }            

        private static void AssertProducts(IEnumerable<Product> products)
        {
            using (var e = products.GetEnumerator())
            {
                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(1, e.Current.ProductId);
                Assert.AreEqual("Chai", e.Current.ProductName);
                Assert.AreEqual("Dharamsala Tea", e.Current.EnglishName);
                Assert.AreEqual("10 boxes x 20 bags", e.Current.QuantityPerUnit);
                Assert.AreEqual(18m, e.Current.UnitPrice);
                Assert.AreEqual(39, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Exotic Liquids", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(2, e.Current.ProductId);
                Assert.AreEqual("Chang", e.Current.ProductName);
                Assert.AreEqual("Tibetan Barley Beer", e.Current.EnglishName);
                Assert.AreEqual("24 - 12 oz bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(19m, e.Current.UnitPrice);
                Assert.AreEqual(17, e.Current.UnitsInStock);
                Assert.AreEqual(40, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Exotic Liquids", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(24, e.Current.ProductId);
                Assert.AreEqual("Guaraná Fantástica", e.Current.ProductName);
                Assert.AreEqual("Guaraná Fantástica Soft Drink", e.Current.EnglishName);
                Assert.AreEqual("12 - 355 ml cans", e.Current.QuantityPerUnit);
                Assert.AreEqual(4.5m, e.Current.UnitPrice);
                Assert.AreEqual(20, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("Refrescos Americanas LTDA", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(34, e.Current.ProductId);
                Assert.AreEqual("Sasquatch Ale", e.Current.ProductName);
                Assert.AreEqual("Sasquatch Ale", e.Current.EnglishName);
                Assert.AreEqual("24 - 12 oz bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(14m, e.Current.UnitPrice);
                Assert.AreEqual(111, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Bigfoot Breweries", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(35, e.Current.ProductId);
                Assert.AreEqual("Steeleye Stout", e.Current.ProductName);
                Assert.AreEqual("Steeleye Stout", e.Current.EnglishName);
                Assert.AreEqual("24 - 12 oz bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(18m, e.Current.UnitPrice);
                Assert.AreEqual(20, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Bigfoot Breweries", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(38, e.Current.ProductId);
                Assert.AreEqual("Côte de Blaye", e.Current.ProductName);
                Assert.AreEqual("Côte de Blaye (Red Bordeaux wine)", e.Current.EnglishName);
                Assert.AreEqual("12 - 75 cl bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(263.5m, e.Current.UnitPrice);
                Assert.AreEqual(17, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Aux joyeux ecclésiastiques", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(39, e.Current.ProductId);
                Assert.AreEqual("Chartreuse verte", e.Current.ProductName);
                Assert.AreEqual("Green Chartreuse (Liqueur)", e.Current.EnglishName);
                Assert.AreEqual("750 cc per bottle", e.Current.QuantityPerUnit);
                Assert.AreEqual(18m, e.Current.UnitPrice);
                Assert.AreEqual(69, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Aux joyeux ecclésiastiques", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(43, e.Current.ProductId);
                Assert.AreEqual("Ipoh Coffee", e.Current.ProductName);
                Assert.AreEqual("Malaysian Coffee", e.Current.EnglishName);
                Assert.AreEqual("16 - 500 g tins", e.Current.QuantityPerUnit);
                Assert.AreEqual(46m, e.Current.UnitPrice);
                Assert.AreEqual(17, e.Current.UnitsInStock);
                Assert.AreEqual(10, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Leka Trading", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(67, e.Current.ProductId);
                Assert.AreEqual("Laughing Lumberjack Lager", e.Current.ProductName);
                Assert.AreEqual("Laughing Lumberjack Lager", e.Current.EnglishName);
                Assert.AreEqual("24 - 12 oz bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(14m, e.Current.UnitPrice);
                Assert.AreEqual(52, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Bigfoot Breweries", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(70, e.Current.ProductId);
                Assert.AreEqual("Outback Lager", e.Current.ProductName);
                Assert.AreEqual("Outback Lager", e.Current.EnglishName);
                Assert.AreEqual("24 - 355 ml bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(15m, e.Current.UnitPrice);
                Assert.AreEqual(15, e.Current.UnitsInStock);
                Assert.AreEqual(10, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Pavlova, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(75, e.Current.ProductId);
                Assert.AreEqual("Rhönbräu Klosterbier", e.Current.ProductName);
                Assert.AreEqual("Rhönbräu Beer", e.Current.EnglishName);
                Assert.AreEqual("24 - 0.5 l bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(7.75m, e.Current.UnitPrice);
                Assert.AreEqual(125, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Plusspar Lebensmittelgroßmärkte AG", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(76, e.Current.ProductId);
                Assert.AreEqual("Lakkalikööri", e.Current.ProductName);
                Assert.AreEqual("Cloudberry Liqueur", e.Current.EnglishName);
                Assert.AreEqual("500 ml", e.Current.QuantityPerUnit);
                Assert.AreEqual(18m, e.Current.UnitPrice);
                Assert.AreEqual(57, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Karkki Oy", e.Current.Supplier);
                Assert.AreEqual("Beverages", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(3, e.Current.ProductId);
                Assert.AreEqual("Aniseed Syrup", e.Current.ProductName);
                Assert.AreEqual("Licorice Syrup", e.Current.EnglishName);
                Assert.AreEqual("12 - 550 ml bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(10m, e.Current.UnitPrice);
                Assert.AreEqual(13, e.Current.UnitsInStock);
                Assert.AreEqual(70, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Exotic Liquids", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(4, e.Current.ProductId);
                Assert.AreEqual("Chef Anton's Cajun Seasoning", e.Current.ProductName);
                Assert.AreEqual("Chef Anton's Cajun Seasoning", e.Current.EnglishName);
                Assert.AreEqual("48 - 6 oz jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(22m, e.Current.UnitPrice);
                Assert.AreEqual(53, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("New Orleans Cajun Delights", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(5, e.Current.ProductId);
                Assert.AreEqual("Chef Anton's Gumbo Mix", e.Current.ProductName);
                Assert.AreEqual("Chef Anton's Gumbo Mix", e.Current.EnglishName);
                Assert.AreEqual("36 boxes", e.Current.QuantityPerUnit);
                Assert.AreEqual(21.35m, e.Current.UnitPrice);
                Assert.AreEqual(0, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("New Orleans Cajun Delights", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(6, e.Current.ProductId);
                Assert.AreEqual("Grandma's Boysenberry Spread", e.Current.ProductName);
                Assert.AreEqual("Grandma's Boysenberry Spread", e.Current.EnglishName);
                Assert.AreEqual("12 - 8 oz jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(25m, e.Current.UnitPrice);
                Assert.AreEqual(120, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Grandma Kelly's Homestead", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(8, e.Current.ProductId);
                Assert.AreEqual("Northwoods Cranberry Sauce", e.Current.ProductName);
                Assert.AreEqual("Northwoods Cranberry Sauce", e.Current.EnglishName);
                Assert.AreEqual("12 - 12 oz jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(40m, e.Current.UnitPrice);
                Assert.AreEqual(6, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Grandma Kelly's Homestead", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(15, e.Current.ProductId);
                Assert.AreEqual("Genen Shouyu", e.Current.ProductName);
                Assert.AreEqual("Lite Sodium Soy Sauce", e.Current.EnglishName);
                Assert.AreEqual("24 - 250 ml bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(15.5m, e.Current.UnitPrice);
                Assert.AreEqual(39, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Mayumi's", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(44, e.Current.ProductId);
                Assert.AreEqual("Gula Malacca", e.Current.ProductName);
                Assert.AreEqual("Malacca Dark Brown Sugar", e.Current.EnglishName);
                Assert.AreEqual("20 - 2 kg bags", e.Current.QuantityPerUnit);
                Assert.AreEqual(19.45m, e.Current.UnitPrice);
                Assert.AreEqual(27, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Leka Trading", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(61, e.Current.ProductId);
                Assert.AreEqual("Sirop d'érable", e.Current.ProductName);
                Assert.AreEqual("Maple Syrup", e.Current.EnglishName);
                Assert.AreEqual("24 - 500 ml bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(28.5m, e.Current.UnitPrice);
                Assert.AreEqual(113, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Forêts d'érables", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(63, e.Current.ProductId);
                Assert.AreEqual("Vegie-spread", e.Current.ProductName);
                Assert.AreEqual("Vegetable Sandwich Spread", e.Current.EnglishName);
                Assert.AreEqual("15 - 625 g jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(43.9m, e.Current.UnitPrice);
                Assert.AreEqual(24, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Pavlova, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(65, e.Current.ProductId);
                Assert.AreEqual("Louisiana Fiery Hot Pepper Sauce", e.Current.ProductName);
                Assert.AreEqual("Louisiana Fiery Hot Pepper Sauce", e.Current.EnglishName);
                Assert.AreEqual("32 - 8 oz bottles", e.Current.QuantityPerUnit);
                Assert.AreEqual(21.05m, e.Current.UnitPrice);
                Assert.AreEqual(76, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("New Orleans Cajun Delights", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(66, e.Current.ProductId);
                Assert.AreEqual("Louisiana Hot Spiced Okra", e.Current.ProductName);
                Assert.AreEqual("Louisiana Hot Spiced Okra", e.Current.EnglishName);
                Assert.AreEqual("24 - 8 oz jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(17m, e.Current.UnitPrice);
                Assert.AreEqual(4, e.Current.UnitsInStock);
                Assert.AreEqual(100, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("New Orleans Cajun Delights", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(77, e.Current.ProductId);
                Assert.AreEqual("Original Frankfurter grüne Soße", e.Current.ProductName);
                Assert.AreEqual("Original Frankfurter Green Sauce", e.Current.EnglishName);
                Assert.AreEqual("12 boxes", e.Current.QuantityPerUnit);
                Assert.AreEqual(13m, e.Current.UnitPrice);
                Assert.AreEqual(32, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Plusspar Lebensmittelgroßmärkte AG", e.Current.Supplier);
                Assert.AreEqual("Condiments", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(16, e.Current.ProductId);
                Assert.AreEqual("Pavlova", e.Current.ProductName);
                Assert.AreEqual("Pavlova Meringue Dessert", e.Current.EnglishName);
                Assert.AreEqual("32 - 500 g boxes", e.Current.QuantityPerUnit);
                Assert.AreEqual(17.45m, e.Current.UnitPrice);
                Assert.AreEqual(29, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Pavlova, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(19, e.Current.ProductId);
                Assert.AreEqual("Teatime Chocolate Biscuits", e.Current.ProductName);
                Assert.AreEqual("Teatime Chocolate Biscuits", e.Current.EnglishName);
                Assert.AreEqual("10 boxes x 12 pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(9.2m, e.Current.UnitPrice);
                Assert.AreEqual(25, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Specialty Biscuits, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(20, e.Current.ProductId);
                Assert.AreEqual("Sir Rodney's Marmalade", e.Current.ProductName);
                Assert.AreEqual("Sir Rodney's Marmalade", e.Current.EnglishName);
                Assert.AreEqual("30 gift boxes", e.Current.QuantityPerUnit);
                Assert.AreEqual(81m, e.Current.UnitPrice);
                Assert.AreEqual(40, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Specialty Biscuits, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(21, e.Current.ProductId);
                Assert.AreEqual("Sir Rodney's Scones", e.Current.ProductName);
                Assert.AreEqual("Sir Rodney's Scones", e.Current.EnglishName);
                Assert.AreEqual("24 pkgs. x 4 pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(10m, e.Current.UnitPrice);
                Assert.AreEqual(3, e.Current.UnitsInStock);
                Assert.AreEqual(40, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Specialty Biscuits, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(25, e.Current.ProductId);
                Assert.AreEqual("NuNuCa Nuß-Nougat-Creme", e.Current.ProductName);
                Assert.AreEqual("NuNuCa Chocolate-Nut Spread", e.Current.EnglishName);
                Assert.AreEqual("20 - 450 g glasses", e.Current.QuantityPerUnit);
                Assert.AreEqual(14m, e.Current.UnitPrice);
                Assert.AreEqual(76, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Heli Süßwaren GmbH & Co. KG", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(26, e.Current.ProductId);
                Assert.AreEqual("Gumbär Gummibärchen", e.Current.ProductName);
                Assert.AreEqual("Gumbär Gummy Bears", e.Current.EnglishName);
                Assert.AreEqual("100 - 250 g bags", e.Current.QuantityPerUnit);
                Assert.AreEqual(31.23m, e.Current.UnitPrice);
                Assert.AreEqual(15, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Heli Süßwaren GmbH & Co. KG", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(27, e.Current.ProductId);
                Assert.AreEqual("Schoggi Schokolade", e.Current.ProductName);
                Assert.AreEqual("Schoggi Chocolate", e.Current.EnglishName);
                Assert.AreEqual("100 - 100 g pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(43.9m, e.Current.UnitPrice);
                Assert.AreEqual(49, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Heli Süßwaren GmbH & Co. KG", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(47, e.Current.ProductId);
                Assert.AreEqual("Zaanse koeken", e.Current.ProductName);
                Assert.AreEqual("Zaanse Cookies", e.Current.EnglishName);
                Assert.AreEqual("10 - 4 oz boxes", e.Current.QuantityPerUnit);
                Assert.AreEqual(9.5m, e.Current.UnitPrice);
                Assert.AreEqual(36, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Zaanse Snoepfabriek", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(48, e.Current.ProductId);
                Assert.AreEqual("Chocolade", e.Current.ProductName);
                Assert.AreEqual("Dutch Chocolate", e.Current.EnglishName);
                Assert.AreEqual("10 pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(12.75m, e.Current.UnitPrice);
                Assert.AreEqual(15, e.Current.UnitsInStock);
                Assert.AreEqual(70, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Zaanse Snoepfabriek", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(49, e.Current.ProductId);
                Assert.AreEqual("Maxilaku", e.Current.ProductName);
                Assert.AreEqual("Licorice", e.Current.EnglishName);
                Assert.AreEqual("24 - 50 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(20m, e.Current.UnitPrice);
                Assert.AreEqual(10, e.Current.UnitsInStock);
                Assert.AreEqual(60, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Karkki Oy", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(50, e.Current.ProductId);
                Assert.AreEqual("Valkoinen suklaa", e.Current.ProductName);
                Assert.AreEqual("White Chocolate", e.Current.EnglishName);
                Assert.AreEqual("12 - 100 g bars", e.Current.QuantityPerUnit);
                Assert.AreEqual(16.25m, e.Current.UnitPrice);
                Assert.AreEqual(65, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Karkki Oy", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(62, e.Current.ProductId);
                Assert.AreEqual("Tarte au sucre", e.Current.ProductName);
                Assert.AreEqual("Sugar Pie", e.Current.EnglishName);
                Assert.AreEqual("48 pies", e.Current.QuantityPerUnit);
                Assert.AreEqual(49.3m, e.Current.UnitPrice);
                Assert.AreEqual(17, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Forêts d'érables", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(68, e.Current.ProductId);
                Assert.AreEqual("Scottish Longbreads", e.Current.ProductName);
                Assert.AreEqual("Scottish Longbreads", e.Current.EnglishName);
                Assert.AreEqual("10 boxes x 8 pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(12.5m, e.Current.UnitPrice);
                Assert.AreEqual(6, e.Current.UnitsInStock);
                Assert.AreEqual(10, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Specialty Biscuits, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Confections", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(11, e.Current.ProductId);
                Assert.AreEqual("Queso Cabrales", e.Current.ProductName);
                Assert.AreEqual("Cabrales Cheese", e.Current.EnglishName);
                Assert.AreEqual("1 kg pkg.", e.Current.QuantityPerUnit);
                Assert.AreEqual(21m, e.Current.UnitPrice);
                Assert.AreEqual(22, e.Current.UnitsInStock);
                Assert.AreEqual(30, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Cooperativa de Quesos 'Las Cabras'", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(12, e.Current.ProductId);
                Assert.AreEqual("Queso Manchego La Pastora", e.Current.ProductName);
                Assert.AreEqual("Manchego La Pastora Cheese", e.Current.EnglishName);
                Assert.AreEqual("10 - 500 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(38m, e.Current.UnitPrice);
                Assert.AreEqual(86, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Cooperativa de Quesos 'Las Cabras'", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(31, e.Current.ProductId);
                Assert.AreEqual("Gorgonzola Telino", e.Current.ProductName);
                Assert.AreEqual("Gorgonzola Telino", e.Current.EnglishName);
                Assert.AreEqual("12 - 100 g pkgs", e.Current.QuantityPerUnit);
                Assert.AreEqual(12.5m, e.Current.UnitPrice);
                Assert.AreEqual(0, e.Current.UnitsInStock);
                Assert.AreEqual(70, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Formaggi Fortini s.r.l.", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(32, e.Current.ProductId);
                Assert.AreEqual("Mascarpone Fabioli", e.Current.ProductName);
                Assert.AreEqual("Mascarpone Fabioli", e.Current.EnglishName);
                Assert.AreEqual("24 - 200 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(32m, e.Current.UnitPrice);
                Assert.AreEqual(9, e.Current.UnitsInStock);
                Assert.AreEqual(40, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Formaggi Fortini s.r.l.", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(33, e.Current.ProductId);
                Assert.AreEqual("Geitost", e.Current.ProductName);
                Assert.AreEqual("Goat Cheese", e.Current.EnglishName);
                Assert.AreEqual("500 g", e.Current.QuantityPerUnit);
                Assert.AreEqual(2.5m, e.Current.UnitPrice);
                Assert.AreEqual(112, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Norske Meierier", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(59, e.Current.ProductId);
                Assert.AreEqual("Raclette Courdavault", e.Current.ProductName);
                Assert.AreEqual("Courdavault Raclette Cheese", e.Current.EnglishName);
                Assert.AreEqual("5 kg pkg.", e.Current.QuantityPerUnit);
                Assert.AreEqual(55m, e.Current.UnitPrice);
                Assert.AreEqual(79, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Gai pâturage", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(60, e.Current.ProductId);
                Assert.AreEqual("Camembert Pierrot", e.Current.ProductName);
                Assert.AreEqual("Pierrot Camembert", e.Current.EnglishName);
                Assert.AreEqual("15 - 300 g rounds", e.Current.QuantityPerUnit);
                Assert.AreEqual(34m, e.Current.UnitPrice);
                Assert.AreEqual(19, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Gai pâturage", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(69, e.Current.ProductId);
                Assert.AreEqual("Gudbrandsdalsost", e.Current.ProductName);
                Assert.AreEqual("Gudbrandsdals Cheese", e.Current.EnglishName);
                Assert.AreEqual("10 kg pkg.", e.Current.QuantityPerUnit);
                Assert.AreEqual(36m, e.Current.UnitPrice);
                Assert.AreEqual(26, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Norske Meierier", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(71, e.Current.ProductId);
                Assert.AreEqual("Fløtemysost", e.Current.ProductName);
                Assert.AreEqual("Fløtemys Cream Cheese", e.Current.EnglishName);
                Assert.AreEqual("10 - 500 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(21.5m, e.Current.UnitPrice);
                Assert.AreEqual(26, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Norske Meierier", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(72, e.Current.ProductId);
                Assert.AreEqual("Mozzarella di Giovanni", e.Current.ProductName);
                Assert.AreEqual("Giovanni's Mozzarella", e.Current.EnglishName);
                Assert.AreEqual("24 - 200 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(34.8m, e.Current.UnitPrice);
                Assert.AreEqual(14, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Formaggi Fortini s.r.l.", e.Current.Supplier);
                Assert.AreEqual("Dairy Products", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(22, e.Current.ProductId);
                Assert.AreEqual("Gustaf's Knäckebröd", e.Current.ProductName);
                Assert.AreEqual("Gustaf's Rye Crisp Bread", e.Current.EnglishName);
                Assert.AreEqual("24 - 500 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(21m, e.Current.UnitPrice);
                Assert.AreEqual(104, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("PB Knäckebröd AB", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(23, e.Current.ProductId);
                Assert.AreEqual("Tunnbröd", e.Current.ProductName);
                Assert.AreEqual("Thin Bread", e.Current.EnglishName);
                Assert.AreEqual("12 - 250 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(9m, e.Current.UnitPrice);
                Assert.AreEqual(61, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("PB Knäckebröd AB", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(42, e.Current.ProductId);
                Assert.AreEqual("Singaporean Hokkien Fried Mee", e.Current.ProductName);
                Assert.AreEqual("Singapore Noodles", e.Current.EnglishName);
                Assert.AreEqual("32 - 1 kg pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(14m, e.Current.UnitPrice);
                Assert.AreEqual(26, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("Leka Trading", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(52, e.Current.ProductId);
                Assert.AreEqual("Filo Mix", e.Current.ProductName);
                Assert.AreEqual("Mix for Greek Filo Dough", e.Current.EnglishName);
                Assert.AreEqual("16 - 2 kg boxes", e.Current.QuantityPerUnit);
                Assert.AreEqual(7m, e.Current.UnitPrice);
                Assert.AreEqual(38, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("G'day, Mate", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(56, e.Current.ProductId);
                Assert.AreEqual("Gnocchi di nonna Alice", e.Current.ProductName);
                Assert.AreEqual("Gramma Alice's Dumplings", e.Current.EnglishName);
                Assert.AreEqual("24 - 250 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(38m, e.Current.UnitPrice);
                Assert.AreEqual(21, e.Current.UnitsInStock);
                Assert.AreEqual(10, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Pasta Buttini s.r.l.", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(57, e.Current.ProductId);
                Assert.AreEqual("Ravioli Angelo", e.Current.ProductName);
                Assert.AreEqual("Angelo Ravioli", e.Current.EnglishName);
                Assert.AreEqual("24 - 250 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(19.5m, e.Current.UnitPrice);
                Assert.AreEqual(36, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Pasta Buttini s.r.l.", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(64, e.Current.ProductId);
                Assert.AreEqual("Wimmers gute Semmelknödel", e.Current.ProductName);
                Assert.AreEqual("Wimmer's Delicious Bread Dumplings", e.Current.EnglishName);
                Assert.AreEqual("20 bags x 4 pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(33.25m, e.Current.UnitPrice);
                Assert.AreEqual(22, e.Current.UnitsInStock);
                Assert.AreEqual(80, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Plusspar Lebensmittelgroßmärkte AG", e.Current.Supplier);
                Assert.AreEqual("Grains/Cereals", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(9, e.Current.ProductId);
                Assert.AreEqual("Mishi Kobe Niku", e.Current.ProductName);
                Assert.AreEqual("Mishi Kobe Beef", e.Current.EnglishName);
                Assert.AreEqual("18 - 500 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(97m, e.Current.UnitPrice);
                Assert.AreEqual(29, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("Tokyo Traders", e.Current.Supplier);
                Assert.AreEqual("Meat/Poultry", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(17, e.Current.ProductId);
                Assert.AreEqual("Alice Mutton", e.Current.ProductName);
                Assert.AreEqual("Alice Springs Lamb", e.Current.EnglishName);
                Assert.AreEqual("20 - 1 kg tins", e.Current.QuantityPerUnit);
                Assert.AreEqual(39m, e.Current.UnitPrice);
                Assert.AreEqual(0, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("Pavlova, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Meat/Poultry", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(29, e.Current.ProductId);
                Assert.AreEqual("Thüringer Rostbratwurst", e.Current.ProductName);
                Assert.AreEqual("Thüringer Sausage", e.Current.EnglishName);
                Assert.AreEqual("50 bags x 30 sausgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(123.79m, e.Current.UnitPrice);
                Assert.AreEqual(0, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("Plusspar Lebensmittelgroßmärkte AG", e.Current.Supplier);
                Assert.AreEqual("Meat/Poultry", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(53, e.Current.ProductId);
                Assert.AreEqual("Perth Pasties", e.Current.ProductName);
                Assert.AreEqual("Perth Meat Pies", e.Current.EnglishName);
                Assert.AreEqual("48 pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(32.8m, e.Current.UnitPrice);
                Assert.AreEqual(0, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("G'day, Mate", e.Current.Supplier);
                Assert.AreEqual("Meat/Poultry", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(54, e.Current.ProductId);
                Assert.AreEqual("Tourtière", e.Current.ProductName);
                Assert.AreEqual("Pork Pie", e.Current.EnglishName);
                Assert.AreEqual("16 pies", e.Current.QuantityPerUnit);
                Assert.AreEqual(7.45m, e.Current.UnitPrice);
                Assert.AreEqual(21, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Ma Maison", e.Current.Supplier);
                Assert.AreEqual("Meat/Poultry", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(55, e.Current.ProductId);
                Assert.AreEqual("Pâté chinois", e.Current.ProductName);
                Assert.AreEqual("Shepard's Pie", e.Current.EnglishName);
                Assert.AreEqual("24 boxes x 2 pies", e.Current.QuantityPerUnit);
                Assert.AreEqual(24m, e.Current.UnitPrice);
                Assert.AreEqual(115, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Ma Maison", e.Current.Supplier);
                Assert.AreEqual("Meat/Poultry", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(7, e.Current.ProductId);
                Assert.AreEqual("Uncle Bob's Organic Dried Pears", e.Current.ProductName);
                Assert.AreEqual("Uncle Bob's Organic Dried Pears", e.Current.EnglishName);
                Assert.AreEqual("12 - 1 lb pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(30m, e.Current.UnitPrice);
                Assert.AreEqual(15, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Grandma Kelly's Homestead", e.Current.Supplier);
                Assert.AreEqual("Produce", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(14, e.Current.ProductId);
                Assert.AreEqual("Tofu", e.Current.ProductName);
                Assert.AreEqual("Bean Curd", e.Current.EnglishName);
                Assert.AreEqual("40 - 100 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(23.25m, e.Current.UnitPrice);
                Assert.AreEqual(35, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Mayumi's", e.Current.Supplier);
                Assert.AreEqual("Produce", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(28, e.Current.ProductId);
                Assert.AreEqual("Rössle Sauerkraut", e.Current.ProductName);
                Assert.AreEqual("Rössle Sauerkraut", e.Current.EnglishName);
                Assert.AreEqual("25 - 825 g cans", e.Current.QuantityPerUnit);
                Assert.AreEqual(45.6m, e.Current.UnitPrice);
                Assert.AreEqual(26, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(true, e.Current.Discontinued);
                Assert.AreEqual("Plusspar Lebensmittelgroßmärkte AG", e.Current.Supplier);
                Assert.AreEqual("Produce", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(51, e.Current.ProductId);
                Assert.AreEqual("Manjimup Dried Apples", e.Current.ProductName);
                Assert.AreEqual("Manjimup Dried Apples", e.Current.EnglishName);
                Assert.AreEqual("50 - 300 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(53m, e.Current.UnitPrice);
                Assert.AreEqual(20, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("G'day, Mate", e.Current.Supplier);
                Assert.AreEqual("Produce", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(74, e.Current.ProductId);
                Assert.AreEqual("Longlife Tofu", e.Current.ProductName);
                Assert.AreEqual("Longlife Bean Curd", e.Current.EnglishName);
                Assert.AreEqual("5 kg pkg.", e.Current.QuantityPerUnit);
                Assert.AreEqual(10m, e.Current.UnitPrice);
                Assert.AreEqual(4, e.Current.UnitsInStock);
                Assert.AreEqual(20, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Tokyo Traders", e.Current.Supplier);
                Assert.AreEqual("Produce", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(10, e.Current.ProductId);
                Assert.AreEqual("Ikura", e.Current.ProductName);
                Assert.AreEqual("Fish Roe", e.Current.EnglishName);
                Assert.AreEqual("12 - 200 ml jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(31m, e.Current.UnitPrice);
                Assert.AreEqual(31, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Tokyo Traders", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(13, e.Current.ProductId);
                Assert.AreEqual("Konbu", e.Current.ProductName);
                Assert.AreEqual("Kelp Seaweed", e.Current.EnglishName);
                Assert.AreEqual("2 kg box", e.Current.QuantityPerUnit);
                Assert.AreEqual(6m, e.Current.UnitPrice);
                Assert.AreEqual(24, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Mayumi's", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(18, e.Current.ProductId);
                Assert.AreEqual("Carnarvon Tigers", e.Current.ProductName);
                Assert.AreEqual("Carnarvon Tiger Prawns", e.Current.EnglishName);
                Assert.AreEqual("16 kg pkg.", e.Current.QuantityPerUnit);
                Assert.AreEqual(62.5m, e.Current.UnitPrice);
                Assert.AreEqual(42, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Pavlova, Ltd.", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(30, e.Current.ProductId);
                Assert.AreEqual("Nord-Ost Matjeshering", e.Current.ProductName);
                Assert.AreEqual("Nord-Ost White Herring", e.Current.EnglishName);
                Assert.AreEqual("10 - 200 g glasses", e.Current.QuantityPerUnit);
                Assert.AreEqual(25.89m, e.Current.UnitPrice);
                Assert.AreEqual(10, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Nord-Ost-Fisch Handelsgesellschaft mbH", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(36, e.Current.ProductId);
                Assert.AreEqual("Inlagd Sill", e.Current.ProductName);
                Assert.AreEqual("Pickled Herring", e.Current.EnglishName);
                Assert.AreEqual("24 - 250 g  jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(19m, e.Current.UnitPrice);
                Assert.AreEqual(112, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Svensk Sjöföda AB", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(37, e.Current.ProductId);
                Assert.AreEqual("Gravad lax", e.Current.ProductName);
                Assert.AreEqual("Gravad Lox", e.Current.EnglishName);
                Assert.AreEqual("12 - 500 g pkgs.", e.Current.QuantityPerUnit);
                Assert.AreEqual(26m, e.Current.UnitPrice);
                Assert.AreEqual(11, e.Current.UnitsInStock);
                Assert.AreEqual(50, e.Current.UnitsOnOrder);
                Assert.AreEqual(25, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Svensk Sjöföda AB", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(40, e.Current.ProductId);
                Assert.AreEqual("Boston Crab Meat", e.Current.ProductName);
                Assert.AreEqual("Boston Crab Meat", e.Current.EnglishName);
                Assert.AreEqual("24 - 4 oz tins", e.Current.QuantityPerUnit);
                Assert.AreEqual(18.4m, e.Current.UnitPrice);
                Assert.AreEqual(123, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(30, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("New England Seafood Cannery", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(41, e.Current.ProductId);
                Assert.AreEqual("Jack's New England Clam Chowder", e.Current.ProductName);
                Assert.AreEqual("Jack's New England Clam Chowder", e.Current.EnglishName);
                Assert.AreEqual("12 - 12 oz cans", e.Current.QuantityPerUnit);
                Assert.AreEqual(9.65m, e.Current.UnitPrice);
                Assert.AreEqual(85, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(10, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("New England Seafood Cannery", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(45, e.Current.ProductId);
                Assert.AreEqual("Røgede sild", e.Current.ProductName);
                Assert.AreEqual("Smoked Herring", e.Current.EnglishName);
                Assert.AreEqual("1k pkg.", e.Current.QuantityPerUnit);
                Assert.AreEqual(9.5m, e.Current.UnitPrice);
                Assert.AreEqual(5, e.Current.UnitsInStock);
                Assert.AreEqual(70, e.Current.UnitsOnOrder);
                Assert.AreEqual(15, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Lyngbysild", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(46, e.Current.ProductId);
                Assert.AreEqual("Spegesild", e.Current.ProductName);
                Assert.AreEqual("Salt Herring", e.Current.EnglishName);
                Assert.AreEqual("4 - 450 g glasses", e.Current.QuantityPerUnit);
                Assert.AreEqual(12m, e.Current.UnitPrice);
                Assert.AreEqual(95, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(0, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Lyngbysild", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(58, e.Current.ProductId);
                Assert.AreEqual("Escargots de Bourgogne", e.Current.ProductName);
                Assert.AreEqual("Escargots from Burgundy", e.Current.EnglishName);
                Assert.AreEqual("24 pieces", e.Current.QuantityPerUnit);
                Assert.AreEqual(13.25m, e.Current.UnitPrice);
                Assert.AreEqual(62, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(20, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Escargots Nouveaux", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsTrue(e.MoveNext());
                Assert.AreEqual(73, e.Current.ProductId);
                Assert.AreEqual("Röd Kaviar", e.Current.ProductName);
                Assert.AreEqual("Red Caviar", e.Current.EnglishName);
                Assert.AreEqual("24 - 150 g jars", e.Current.QuantityPerUnit);
                Assert.AreEqual(15m, e.Current.UnitPrice);
                Assert.AreEqual(101, e.Current.UnitsInStock);
                Assert.AreEqual(0, e.Current.UnitsOnOrder);
                Assert.AreEqual(5, e.Current.ReorderLevel);
                Assert.AreEqual(false, e.Current.Discontinued);
                Assert.AreEqual("Svensk Sjöföda AB", e.Current.Supplier);
                Assert.AreEqual("Seafood", e.Current.Category);

                Assert.IsFalse(e.MoveNext());
            }
        }
    }
}
