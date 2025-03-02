namespace DSALibrary
{
    public class GraphTraversal
    {

        public void BFS(MyLinkedList<int>[] graph, int startNode)
        {
            // Custom queue implementation
            int[] queue = new int[graph.Length];
            int front = 0, rear = 0;

            // Visited array to track visited nodes
            bool[] visited = new bool[graph.Length];

            // Enqueue the start node
            queue[rear] = startNode;
            rear++;
            visited[startNode] = true;

            while (front < rear)
            {
                // Dequeue the front node
                int current = queue[front];
                front++;

                // Print the current node
                Console.WriteLine(current);

                // Traverse neighbors
                Node<int> neighbor = graph[current].GetHead(); // get the head of adjacency list
                while (neighbor != null)
                {
                    if (!visited[neighbor.Data])
                    {
                        //enqueue unvisited neighbor
                        queue[rear] = neighbor.Data;
                        rear++;
                        visited[neighbor.Data] = true;
                    }
                    neighbor = neighbor.Next; // move to the next node
                }
            }

        }
        public void DFS(MyLinkedList<int>[] graph, int startNode)
        {
            // Custom stack implementation
            int[] stack = new int[graph.Length];
            int top = -1;

            // Visited array to track visited nodes
            bool[] visited = new bool[graph.Length];

            // Push the start node onto the stack
            top++;
            stack[top] = startNode;

            while (top >= 0)
            {
                // Pop the top node
                int current = stack[top];
                top--;

                // If the node has not been visited, process it
                if (!visited[current])
                {
                    Console.WriteLine(current);
                    visited[current] = true;

                    // Push all unvisited neighbors onto the stack
                    Node<int> neighbor = graph[current].GetHead(); // Get the head of adjacency list
                    while (neighbor != null)//traverse through linked list
                    {
                        if (!visited[neighbor.Data])
                        {
                            top++; //push neighbor onto stack
                            stack[top] = neighbor.Data;
                        }
                        neighbor = neighbor.Next;
                    }

                }
            }
        }

    }

    public class SortingAlgorithms
    {
        // Bubble Sort
        // Bubble Sort for double[]
        public static void bubbleSort(double[] array)
        {
            int n = array.Length;
            double temp;
            bool swapped;   // To check if we need another iteration

            for (int i = 0; i < n - 1; i++)      // Go through all elements
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++) // Inner loop
                {
                    if (array[j] > array[j + 1]) // Compare adjacent elements
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped) break;  // If no elements were swapped, array is already sorted
            }
        }

        // Quick Sort
        // Quick Sort for double[]
        public static void QuickSort(double[] array, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(array, low, high);
                QuickSort(array, low, pivot - 1);  // Before pivot
                QuickSort(array, pivot + 1, high); // After pivot
            }
        }

        private static int Partition(double[] array, int low, int high)
        {
            double pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            Swap(array, i + 1, high);
            return i + 1;
        }

        private static void Swap(double[] array, int i, int j)
        {
            double temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        // Merge Sort
        // Merge Sort for double[]
        public static void MergeSort(double[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);
                Merge(array, left, middle, right);
            }
        }

        private static void Merge(double[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            double[] L = new double[n1];
            double[] R = new double[n2];

            // Copy data into temp arrays
            for (int i1 = 0; i1 < n1; i1++)
                L[i1] = array[left + i1];
            for (int i2 = 0; i2 < n2; i2++)
                R[i2] = array[mid + 1 + i2];

            int i = 0, j = 0, k = left;

            // Merge the temp arrays back into the original array
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    array[k] = L[i];
                    i++;
                }
                else
                {
                    array[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy the remaining elements of L[] and R[]
            while (i < n1)
            {
                array[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = R[j];
                j++;
                k++;
            }
        }
    }

    public class SearchingAlgorithms
    {


    }
    public class BST<T> where T : IComparable<T>
    {
        private BSTNode<T> root;

        public void Insert(T data)
        {
            root = InsertRecursive(root, data);
        }

        private BSTNode<T> InsertRecursive(BSTNode<T> node, T data)
        {
            if (node == null)
            {
                return new BSTNode<T>(data);
            }

            if (data.CompareTo(node.Data) < 0)
            {
                node.Left = InsertRecursive(node.Left, data);
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                node.Right = InsertRecursive(node.Right, data);
            }
            else
            {
                // 👇 If Duplicate Farmer Name Found
                // Console.WriteLine($"[yellow]Duplicate Farmer Found 🔄 Updating Data...[/]");

                // FarmerHarvest Update Logic
                if (typeof(T) == typeof(FarmerHarvest))
                {
                    FarmerHarvest existingHarvest = node.Data as FarmerHarvest;
                    FarmerHarvest newHarvest = data as FarmerHarvest;

                    if (existingHarvest != null && newHarvest != null)
                    {
                        // Add Quantity
                        existingHarvest.Quantitykg += newHarvest.Quantitykg;

                        // Update Date if New Date is Latest
                        if (newHarvest.Date > existingHarvest.Date)
                        {
                            existingHarvest.Date = newHarvest.Date;
                        }
                    }

                }
                else if (typeof(T) == typeof(CropHarvest))
                {
                    CropHarvest existing = node.Data as CropHarvest;
                    CropHarvest incoming = data as CropHarvest;

                    if (existing != null && incoming != null)
                    {
                        existing.Quantitykg += incoming.Quantitykg;

                        if (incoming.Date > existing.Date)
                        {
                            existing.Date = incoming.Date;
                        }
                    }
                }
                else if (typeof(T) == typeof(RegionHarvest))
                {
                    RegionHarvest existing = node.Data as RegionHarvest;
                    RegionHarvest incoming = data as RegionHarvest;

                    if (existing != null && incoming != null)
                    {
                        existing.Quantitykg += incoming.Quantitykg;

                        if (incoming.Date > existing.Date)
                        {
                            existing.Date = incoming.Date;
                        }
                    }
                }
            }

            return node;
        }
        // In-order traversal method to collect elements into a list
        public List<T> ToList()
        {
            List<T> list = new List<T>();
            InOrderTraversal(root, list);
            return list;
        }
        private void InOrderTraversal(BSTNode<T> node, List<T> list)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, list);
                list.Add(node.Data);  // Add the node's data to the list
                InOrderTraversal(node.Right, list);
            }
        }

        public void InOrderTraversal()
        {
            if (root == null)
            {
                Console.WriteLine("[yellow]BST is Empty ❌[/]");
                return;
            }

            Console.WriteLine("[green]In-Order Traversal 🔥:[/]");
            InOrderRecursive(root);
            Console.WriteLine();
        }

        private void InOrderRecursive(BSTNode<T> node)
        {
            if (node != null)
            {
                InOrderRecursive(node.Left);
                Console.WriteLine(node.Data);
                InOrderRecursive(node.Right);
            }
        }

        public BSTNode<T> GetRoot()
        {
            return root;
        }

    }

    public class BSTNode<T>
    {
        public T Data { get; set; }
        public BSTNode<T> Left { get; set; }
        public BSTNode<T> Right { get; set; }

        public BSTNode(T data)
        {
            Data = data;
            Left = Right = null;
        }
    }
    public class MyLinkedList<T> //  type parameter T
    {
        private Node<T> head;

        // Add a new node to the end of the list
        public void Add(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode; // If list is empty, set head to new node
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null) // Traverse to the last node
                {
                    current = current.Next;
                }
                current.Next = newNode; // Link the last node to the new node
            }
            // Console.WriteLine($"Added: {data}");
        }

        // Delete the first node with the specified data
        public void Delete(T data)
        {
            if (head == null)
            {
                // Console.WriteLine("List is empty.");
                return;
            }

            // Check if the head node needs to be removed
            if (head.Data.Equals(data))
            {
                head = head.Next; // Move the head to the next node
                Console.WriteLine($"Deleted: {data}");
                return;
            }

            // Traverse the list to find the node to delete
            Node<T> current = head;
            while (current.Next != null && !current.Next.Data.Equals(data))
            {
                current = current.Next;
            }

            if (current.Next == null)
            {
                Console.WriteLine($"Element '{data}' not found.");
            }
            else
            {
                current.Next = current.Next.Next; // Remove the node
                Console.WriteLine($"Deleted: {data}");
            }
        }

        // Display all nodes in the list
        public void Display()
        {
            if (head == null)
            {
                Console.WriteLine("The list is empty.");
                return;
            }

            Node<T> current = head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        // To access the head node
        public Node<T> GetHead()
        { return head; }
    }

    // Generic Node class
    public class Node<T>
    {
        public T Data { get; set; } // Data of type T
        public Node<T> Next { get; set; } // Reference to the next node

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }


}