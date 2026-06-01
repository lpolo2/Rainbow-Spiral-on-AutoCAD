using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

// CRITICAL: This must be OUTSIDE the namespace and match your exact namespace name!
[assembly: CommandClass(typeof(AcadAutomationPlugin.HelloCommand))]

namespace AcadAutomationPlugin
{
    public class HelloCommand
    {
        [CommandMethod("DRAWLINE")]
        public void DrawMyLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Point3d startPt = new Point3d(0, 0, 0);
                Point3d endPt = new Point3d(10, 10, 0);

                using (Line acLine = new Line(startPt, endPt))
                {
                    acLine.ColorIndex = 1; // Red
                    btr.AppendEntity(acLine);
                    tr.AddNewlyCreatedDBObject(acLine, true);
                }
                tr.Commit();
            }
            doc.Editor.UpdateScreen();
        }
    }
}