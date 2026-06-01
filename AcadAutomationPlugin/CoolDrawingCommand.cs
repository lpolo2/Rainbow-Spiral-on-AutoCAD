using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

// Register this class so AutoCAD can find its commands
[assembly: CommandClass(typeof(AcadAutomationPlugin.CoolDrawingCommand))]

namespace AcadAutomationPlugin
{
    public class CoolDrawingCommand
    {
        [CommandMethod("DRAW3DSPIRAL")]
        public void DrawCoolSpiral()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                // Parameters for our math spiral
                int totalPoints = 150;     // Number of spheres to draw
                double radiusGrowRate = 0.5; // How fast the spiral spreads outward
                double heightGrowRate = 0.3; // How fast it climbs in 3D space (Z-axis)
                double sphereRadius = 0.8;   // Size of each drawn sphere

                for (int i = 0; i < totalPoints; i++)
                {
                    // Theta controls the angle of rotation (using radians)
                    double theta = i * 0.2; 
                    
                    // Archimedean Spiral Math: r = a * theta
                    double r = radiusGrowRate * theta;

                    // Convert Polar Coordinates (r, theta) to Cartesian (X, Y, Z)
                    double x = r * Math.Cos(theta);
                    double y = r * Math.Sin(theta);
                    double z = i * heightGrowRate; // Climbing upward

                    Point3d centerPoint = new Point3d(x, y, z);

                    // Create a 3D Solid Sphere at that coordinate
                    using (Solid3d sphere = new Solid3d())
                    {
                        sphere.CreateSphere(sphereRadius);
                        
                        // Move the sphere from (0,0,0) to its calculated spiral position
                        Matrix3d moveMatrix = Matrix3d.Displacement(centerPoint - Point3d.Origin);
                        sphere.TransformBy(moveMatrix);

                        // Cycle through AutoCAD colors (1 to 7) to create a rainbow effect
                        short colorIndex = (short)((i % 7) + 1);
                        sphere.ColorIndex = colorIndex;

                        // Append to database
                        btr.AppendEntity(sphere);
                        tr.AddNewlyCreatedDBObject(sphere, true);
                    }
                }

                tr.Commit();
                doc.Editor.WriteMessage("\n3D Parametric Spiral generated successfully!\n");
            }
            doc.Editor.UpdateScreen();
        }
    }
}