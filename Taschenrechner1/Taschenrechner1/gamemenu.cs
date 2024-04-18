using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner1
{
    class gamemenu: menu
    {
        public override void DisplayMenu()
        {
            
            Console.WriteLine("Welcome to Taschenrechner");
            Console.WriteLine("-------------------------");
            Console.WriteLine("[*]input:");
            loop();
        }

        public string getInput()
        {
            // Eingabe vom User
            string input = Console.ReadLine();

            // Entfernen von Leerzeichen aus der Eingabe
            input = input.Replace(" ", "");

            // Überprüfen, ob die Eingabe leer ist
            if (checkInput(input) == true)  { return input;}
            else {  return "";}
        }

        public bool checkInput(string input)
        {
            // Überprüfen, ob die Eingabe leer ist
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Error: Eingabe ist falsch");
                return false;
            }
            else{ return true; }
        }

        public char[] splitOperators()
        {
            char[] operators = { '+', '-', '*', '/'};
            return operators;
        }

        public string[] splitOperands(string input, char[] operators)
        {
            string[] parts = input.Split(operators);
            return parts;
        }

        public List<double> splitInputOperands()
        {
            // Trennen der Eingabe in Operators 
            List<double> operands = new List<double>();
            return operands;
        }

        public List<char> splitInputOperators()
        {
            // Trennen der Eingabe in Operanden
            List<char> operationList = new List<char>();
            return operationList;
        }

        public void extractOperandsOperators(string input, char[] operators, List<double> operands, List<char> operationList)
        {
            int currentIndex = 0;
            while(currentIndex < input.Length)
            {

                string currentOperand = "";

                // Extrahieren des aktuellen Operanden also z.B er nimmt die operatoren aus der gleichung 2 + 2 dann nimmt der die + raus
                while ( currentIndex < input.Length && !operators.Contains(input[currentIndex]))
                {
                    currentOperand += input[currentIndex];
                    currentIndex++;
                }

                // Hinzufügen des Operanden zur Liste
                
                if (!string.IsNullOrWhiteSpace(currentOperand)) 
                {
                    // Hinzufügen des Operanden zur Liste
                    operands.Add(double.Parse(currentOperand));
                }

                

                // Überprüfen, ob das Ende der Eingabe erreicht wurde
                if (currentIndex >= input.Length){ break; }

               
                // Hinzufügen des aktuellen Operators zur Liste
                operationList.Add(input[currentIndex]);
                currentIndex++;

            }
        }

        public double calculate(string input)
        {
            // Durchführen der Berechnung
            // Punkt vor Strich, Klammer zuerst Regel
            // Man muss vor einem Klammer immer ein Mal setzen

            char[] operators = splitOperators();
            string[] parts = splitOperands(input, operators);
            List<double> operands = splitInputOperands();
            List<char> operationList = splitInputOperators();

            extractOperandsOperators(input, operators, operands, operationList);

            for (int i = 0; i < operationList.Count; i++)
            {
                if (operationList[i] == '*' || operationList[i] == '/')
                {
                    double result;
                    if (operationList[i] == '*')
                    {
                        result = operands[i] * operands[i + 1];
                    }
                    else
                    {
                        if (operands[i + 1] == 0)
                        {
                            Console.WriteLine("Durch Null nicht divideieren");
                            return double.NaN;
                        }

                        result = operands[i] / operands[i + 1];
                    }

                    // Entfernen der bereits verarbeiteten Operanden und Operatoren
                    operands.RemoveAt(i);
                    operands[i] = result;
                    operationList.RemoveAt(i);
                    i--; // Um die Position korrekt zu aktualisieren, da ein Element entfernt wurde



                }

            }

            // Dann die Addition und Subtraktion durchführen
            double finalResult = operands[0];
            for (int i = 0; i < operationList.Count; i++) 
            {
                if (operationList[i] == '+')
                {
                    finalResult += operands[i + 1];
                }
                else
                {
                    finalResult -= operands[i + 1];
                }
            }

            return finalResult;
        }

        public double calculatedKlammer(string input)
        {
            // Suchen nach innersten Klammernpaaren und Berechnen des inneren Ausdrucks
            // ( 2 + 2)
            while (input.Contains('('))
            {
                int startPoint = input.IndexOf('(');
                int endPoint = input.IndexOf(')'); 


                if(endPoint == -1)
                {
                    Console.WriteLine("Du hast vergessen die Klammer zu schließen )");
                    return double.NaN; // ( 2 + 2 | Die Rechnung ohne endklammer wird einfach 0 also: 2 + 2 + 2 + (2 + 2 | 6 + 0
                }

                // Ersetzen des inneren Ausdrucks durch das Ergebnis
                string innerGleichung = input.Substring(startPoint + 1, endPoint - startPoint - 1);

                double innerResult = calculatedKlammer(innerGleichung);

                // Ersetzen des inneren Ausdrucks durch das Ergebnis
                input  = input.Substring(0, startPoint) + innerResult.ToString() + input.Substring(endPoint+1);
                
            }

            // Jetzt, da keine Klammern mehr vorhanden sind, führen wir die verbleibenden Operationen aus
            return calculate(input);
        }

        public void loop()
        {

            string input = getInput();

            // Berechnen des Ergebnisses
            double result = calculatedKlammer(input);

            // Ausgabe des Ergebnisses
            Console.WriteLine($"Result: {result}");

            

        }

    }
}
