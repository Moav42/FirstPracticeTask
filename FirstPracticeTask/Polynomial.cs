using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPracticeTask
{
    /* This class is designed to work with standard polynomials with single root*/
    public class Polynomial
    {
        // Key it's the exponent of terms, the value its coefficient
        private Dictionary<int, double> polynomDic = new Dictionary<int, double>();
        private double[] coefficientValu;
        private int[] exponentValue;

        public Polynomial() {  }
        public Polynomial( double[] coefficientValu,  int[] exponentValue)
        {
            CreatePolynomial(coefficientValu, exponentValue);
            this.coefficientValu = coefficientValu;
            this.exponentValue = exponentValue;
        }

        private void CreatePolynomial(double[] Coefficient, int[] exponent)
        {
            if(Coefficient.Length != exponent.Length)
            {
                throw new ArgumentException("Count of Coefficients must be equel to Count of Exponent");
            }

            for (int i = 0; i < Coefficient.Length; i++)
            {
                polynomDic.Add(exponent[i], Coefficient[i]);               
                
            }
        }     

        public void ChangeCoefficientValue(int exponentOfTermsToChange, double newValue)
        {
            if (!polynomDic.ContainsKey(exponentOfTermsToChange))
            {
                throw new ArgumentException("Polunomial dosen't contain a member with such a degree");
            }
            polynomDic[exponentOfTermsToChange] = newValue;
        }

        public void ChangeExponentValue( int expontToChange, int newValuseOfExponent)
        {
            if (!polynomDic.ContainsKey(expontToChange))
            {
                throw new ArgumentException("Polunomial dosen't contain a member with such a degree");
            }
            double valueOfCoefficient = polynomDic[expontToChange];

            polynomDic.Remove(expontToChange);

            if (polynomDic.ContainsKey(newValuseOfExponent))
            {
                polynomDic[newValuseOfExponent] += valueOfCoefficient;
            }

            else
            {
                polynomDic.Add(newValuseOfExponent, valueOfCoefficient);
            }
        }
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            Polynomial newPolynomial = new Polynomial();
            int maxKey = p1.polynomDic.Keys.Max() > p2.polynomDic.Keys.Max() ? p1.polynomDic.Keys.Max() : p2.polynomDic.Keys.Max();
            

            for (int i = maxKey; i >= 0; i--)
            {
                if (p1.polynomDic.ContainsKey(i) && p2.polynomDic.ContainsKey(i))
                {
                    double coefValue = p1.polynomDic[i] + p2.polynomDic[i];
                    newPolynomial.polynomDic.Add(i, coefValue);
                }
                else if (p1.polynomDic.ContainsKey(i))
                {
                    double coefValue = p1.polynomDic[i];
                    newPolynomial.polynomDic.Add(i, coefValue);
                }
                else if (p2.polynomDic.ContainsKey(i))
                {
                    double coefValue = p2.polynomDic[i];
                    newPolynomial.polynomDic.Add(i, coefValue);
                }
            }
            return newPolynomial;
        }
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            Polynomial newPolynomial = new Polynomial();
            int maxKey = p1.polynomDic.Keys.Max() > p2.polynomDic.Keys.Max() ? p1.polynomDic.Keys.Max() : p2.polynomDic.Keys.Max();


            for (int i = maxKey; i >= 0; i--)
            {
                if (p1.polynomDic.ContainsKey(i) && p2.polynomDic.ContainsKey(i))
                {
                    double coefValue = p1.polynomDic[i] - p2.polynomDic[i];
                    newPolynomial.polynomDic.Add(i, coefValue);
                }
                else if (p1.polynomDic.ContainsKey(i))
                {
                    double coefValue = p1.polynomDic[i];
                    newPolynomial.polynomDic.Add(i, coefValue);
                }
                else if (p2.polynomDic.ContainsKey(i))
                {
                    double coefValue = - p2.polynomDic[i];
                    newPolynomial.polynomDic.Add(i, coefValue);
                }
            }
            return newPolynomial;
        }
        public static Polynomial operator *(Polynomial p1, double val)
        {
            double[] coefVal = p1.coefficientValu;
            for (int i = 0; i < p1.coefficientValu.Length; i++)
            {
                coefVal[i] = coefVal[i] * val;
            }
            Polynomial newPolynomial = new Polynomial(coefVal, p1.exponentValue);
            return newPolynomial;
        }
        public static Polynomial operator *(double val, Polynomial p1)
        {
            return p1 * val;
        }
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            int[] expFromP1 = p1.exponentValue;
            int[] expFromP2 = p2.exponentValue;

            double[] koefFromP1 = p1.coefficientValu;
            double[] koefFromP2 = p2.coefficientValu;

            Polynomial newPolynomial = new Polynomial();

            for (int i = 0; i < expFromP1.Length; i++)
            {
                int curExpVal = 0;
                double curKoefVal = 0;
                for (int j = 0; j < koefFromP2.Length; j++)
                {
                    if (p2.polynomDic.ContainsKey(j) && p1.polynomDic.ContainsKey(i))
                    {
                        curExpVal = expFromP1[i] + expFromP2[j];
                        curKoefVal = koefFromP1[i] * koefFromP2[j];
                    }
                    else if (p1.polynomDic.ContainsKey(j))
                    {
                        curExpVal = expFromP1[i];
                        curKoefVal = koefFromP1[i];
                    }
                    else if (p2.polynomDic.ContainsKey(j))
                    {
                        curExpVal = expFromP2[j];
                        curKoefVal = koefFromP2[j];
                    }
                    if (newPolynomial.polynomDic.ContainsKey(curExpVal))
                    {
                        newPolynomial.polynomDic[curExpVal] += curKoefVal;
                    }
                    else
                    {
                        newPolynomial.polynomDic.Add(curExpVal, curKoefVal);
                    }
                }
            }     
            return newPolynomial;
        }
        public void PrintToConsole()
        {
            foreach (var item in polynomDic)
            {
                if(item.Value > 0)
                {
                    Console.Write($"+{item.Value}x^{item.Key}");
                }
                else
                {
                    Console.Write($"{item.Value}x^{item.Key}");
                }
            }
            Console.WriteLine();
        }

    }
}
