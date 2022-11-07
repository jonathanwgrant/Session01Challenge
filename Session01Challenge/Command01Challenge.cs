#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Session01Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Command01Challenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Variables //
            string divBy3 = "FIZZ";
            string divBy5 = "BUZZ";
            string divBy3and5 = divBy3 + divBy5;
            string result = "";

            int testCase = 1;

            XYZ header1 = new XYZ(9.5, 10.25, 0);
            XYZ header2 = new XYZ(10, 10.25, 0);
            XYZ myPoint = new XYZ(10, 10, 0);
            XYZ offset = new XYZ(0, -.15, 0);
            XYZ offsetTestCase = new XYZ(-.5, 0, 0);

            // Filtered Element Collector //

            FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(TextNoteType));

            // FizzBuzz Tests //

            Transaction t = new Transaction(doc);
            t.Start("Run FizzBuzz Tests");
            TextNote myTextNote0 = TextNote.Create(doc, doc.ActiveView.Id, header1, "Test Case", collector.FirstElementId());
            TextNote myTextNote1 = TextNote.Create(doc, doc.ActiveView.Id, header2, "Result", collector.FirstElementId());
            for (int i = 1; i <= 100; i++)
            {
                // This is best viewed at 1-1/2" = 1'-0" Scale //
                doc.ActiveView.Scale = 5;

                int test3 = testCase % 3;
                int test5 = testCase % 5;
                if (test3 == 0 && test5 == 0)
                {
                    result = divBy3and5;
                }
                else if (test3 == 0)
                {
                    result = divBy3;
                }
                else if (test5 == 0)
                    result = divBy5;
                else
                {
                    result = testCase.ToString();
                }

                TextNote myTextNote2 = TextNote.Create(doc, doc.ActiveView.Id, myPoint.Add(offsetTestCase), testCase.ToString(), collector.FirstElementId());
                TextNote myTextNote3 = TextNote.Create(doc, doc.ActiveView.Id, myPoint, result, collector.FirstElementId());

                testCase = testCase + 1;
                myPoint = myPoint.Add(offset);
            }
            t.Commit();

            return Result.Succeeded;
        }
    }
}
