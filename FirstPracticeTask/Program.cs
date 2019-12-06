using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstPracticeTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------Matrix---------------------------------");
            Console.WriteLine("Matrix 1: ");
            Matrix m1 = new Matrix(new double[3, 3] { { 21, 3.1, 2 }, { 2.4, 26, 2.8 }, { 8.4, 42, 2.8 } });           
            m1.PrintToConsole();           

            Console.WriteLine("Matrix 2: ");
            Matrix m2 = new Matrix(new double[3, 3] { { 42, 71, 3.3 }, { 52, 22, 13 }, { 21, 21, 3.5 } });
            m2.PrintToConsole();

            Console.WriteLine("Added matrix1 to matrix2: ");
            Matrix m3 = m1 + m2;
            m3.PrintToConsole();
          

            Console.WriteLine("Multiplication of matrix1 to matrix2: ");
            m3 = m2 * m2;
            m3.PrintToConsole();
            Matrix matrix4 = m1.Clone() as Matrix;
            m1[0, 0] = 500;
       
     
            matrix4.PrintToConsole();
            Console.WriteLine(new string('-',100));
            Console.WriteLine("Compare Matrix1 to clone of Matrix1: ");
            Console.WriteLine(m1.CompareTo(matrix4));

            Console.WriteLine();

            Console.WriteLine("Compare Matrix1 to Matrix3: ");
            Console.WriteLine(m1.CompareTo(m3));

            Console.WriteLine();
            Console.WriteLine("Serialization of matrix3, and print deserialazed matrix: ");
            m3.Serialize();
            Matrix m5 = Matrix.Deserialize();
            m5.PrintToConsole();

            Console.WriteLine();
            Console.WriteLine("Try to compere matrix to int: ");
            try
            {
                m3.CompareTo(42);
            }
            catch (MatrixException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();

            Console.WriteLine("---------------------------------Polynomial---------------------------------");

            Console.WriteLine("First Polynomial: ");

            double[] terms = { 42, -15, 10, -5, 2 };
            int[] expont = { 7, 3, 2, 1, 0 };
            Polynomial p1 = new Polynomial( terms, expont);

            p1.PrintToConsole();
            Console.WriteLine();

            Console.WriteLine("Second Polynomial: ");

            double[] terms2 = { 21, 21, -33,  3 };
            int[] expont2 = { 5, 3, 2, 0 };
            Polynomial p2 = new Polynomial(terms2, expont2);

            p2.PrintToConsole();
            Console.WriteLine();

    
            Console.WriteLine("Added Polynomial: ");
            Polynomial p3 = p1 + p2;
            p3.PrintToConsole();

            Console.WriteLine();

            Console.WriteLine("Multiplication of Polynomial: ");
            p3 = p1 * p2;
            p3.PrintToConsole();

            Console.WriteLine();

            Console.WriteLine("Second Polynomial after chang of exponent and coefficient: ");
            p2.ChangeExponentValue(5, 666);
            p2.ChangeCoefficientValue(666, 9000);
            p2.PrintToConsole();        

            Console.ReadLine();
        }
    }
}
