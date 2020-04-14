using System;
using System.Linq;
using System.Collections.Generic;

namespace DijkstraAlgo
{
    class Program

    {

        static double infinity = double.PositiveInfinity;

        static void Main(string[] args)

        {

            /*

             * Graphmatrix die den kompletten Graphen abbildet.

             * Jeder aufgeführte Knoten enthält die Kosten zu den anderen Knoten.

             * Besteht keine verbindung betragen die Kosten 0.

             */

            int[][] graphMatrix = new int[][] { new int[] { 0, 0, 0, 3, 12 },

                                                new int[] { 0, 0, 2, 0, 0},

                                                new int[] { 0, 0, 0, -2, 0},

                                                new int[] { 0, 5, 3, 0, 0},

                                                new int[] { 0, 0, 7, 0, 0} };



            /*

             * Array für Knoten initialisieren. Die Länge der Liste ergibt sich aus der

             * ersten Dimension der graphMatrix die jeweils einen Knoten darstellt.

             */

            Node[] nodes = new Node[graphMatrix.GetLength(0)];



            // Wert des Startknotens

            int startNodeNumber = 0;



            /*

             * Das Knotenfeld wird durchlaufen. 

             * Der Zähler dient als Name für jeden initialisierten Knoten.

             */

            for (int i = 0; i < nodes.Length; i++) nodes[i] = new Node() { Name = i.ToString() };



            // Dijkstra Algorythmus aufrufen

            Dijkstra(nodes, graphMatrix, startNodeNumber);



            // Ergebnisse ausdrucken

            PrintPath(nodes[0], nodes[2]);



            Console.ReadKey();

        }



        public static void Dijkstra(Node[] nodes, int[][] graphMatrix, int startNodeNumber)

        {

            /*

             * Die Prioritätsliste bestimmt die Reihenfolge in der die Knoten

             * abgearbeitet werden.

             */

            List<Node> priorityList = new List<Node>();



            InitializingNodeContent(priorityList, nodes, nodes[startNodeNumber]);



            /*

             * Solange die Prioritätsliste noch Knoten enthält werden

             * diese weiter Verarbeitet.

             */

            while (priorityList.Count > 0)

            {

                /*

                 * Der aktuelle Knoten der Liste befindet sich immer an der

                 * ersten Stelle und wird zur Verarbeitung ausgewählt.

                 */

                Node node = priorityList[0];

                /*

                 * Die zweiten Dimension der graphMatrix beschreibt zu

                 * welchen anderen Knoten der aktuelle Knoten verbunden ist.

                 * Wo Kosten eingetragen sind gibt es eine Verdindung. Die

                 * Feldnummer des Feldes indem Kosten eingetragen sind ist

                 * gleichzeitig der Name des Knotens mit dem man verbunden ist.

                 */

                for (int n = 0; n < graphMatrix[Convert.ToInt32(node.Name)].Length; n++)

                {

                    // Alles größer 0 stellt eine Verbindung dar. Der Knoten darf zudem noch nicht verarbeitet sein

                    if (graphMatrix[Convert.ToInt32(node.Name)][n] > 0)

                    {

                        SetLowestCosts(nodes[Convert.ToInt32(node.Name)], nodes[n], graphMatrix[Convert.ToInt32(node.Name)][n]);

                    }

                }



                /*

                 * Der aktuelle Knoten der Liste gilt nun als Verarbeitet und wird aus

                 * der Prioritätsliste entfernt. Danach wird die Liste der Knoten in aufsteigender

                 * Reihenfolge ihrer Kosten neu sortiert. Der günstigste Knoten wird daher im

                 * nächsten durchlauf als aktuelles Element ausgewählt und verarbeitet.

                 */

                priorityList.RemoveAt(0);

                priorityList = (from element in priorityList orderby element.Costs select element).ToList();

            }

        }



        // InitializingNodeContent beschreibt Knoten mit Defaultwerten.

        public static void InitializingNodeContent(List<Node> priorityList, Node[] nodes, Node startNode)

        {

            /*

             * Die Kostenproperty wird für alle Knoten auf unendlich gesetzt.

             * Dieser Wert wird später mit den ermittelten Kosten überschrieben.

             * Die Vorgänger stehen hier noch nicht fest und werden mit Null initialisiert.

             */

            foreach (Node node in nodes)

            {

                node.Costs = infinity;

                node.Parent = null;

                priorityList.Add(node);

            }



            // Der Startknoten ist unser Ausgangspunkt und wird daher mit 0 Kosten initialisiert.

            startNode.Costs = 0;

        }



        public static void SetLowestCosts(Node predecessorNode, Node successorNode, double distanceCosts)

        {

            // Wenn der Nachfolger höhere Kosten hat als die Kosten des Vorgängers + die Distanzkosten zum Nachfolger

            // ist die Verbindung vom aktuellen Vorgänger zum Nachfolger günstiger. Der aktuelle Knoten muss dann im

            // Nachfolger als Elternteil eingetragen und die Distanzkosten des Elternteilt als Kosten des Nachfolgers übernommen werden.

            if (successorNode.Costs > predecessorNode.Costs + distanceCosts)

            {

                successorNode.Costs = predecessorNode.Costs + distanceCosts;

                successorNode.Parent = predecessorNode;

            }

        }



        /*

         * Die Ergebnisliste wird rekursiv durchlaufen und ausgegeben.

         * Im Rekursionsfall nähert sich der Endknoten dabei dem Startknoten an bis beide auf das

         * selbe Element verweisen. Im Basisfall wird ein das Element der Liste auf dem Bildschirm

         * ausgegeben.

         */

        public static void PrintPath(Node startNode, Node endNode)

        {

            if (endNode != startNode)

            {

                PrintPath(startNode, endNode.Parent);

                Console.WriteLine("Node {0} bisherige Kosten: {1}", endNode.Name, endNode.Costs);

            }
            else

                Console.WriteLine("Node {0} bisherige Kosten: {1}", startNode.Name, endNode.Costs);

        }

    }
}
