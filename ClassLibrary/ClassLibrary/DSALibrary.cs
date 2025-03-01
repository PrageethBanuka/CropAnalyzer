namespace DSALibrary
{
    public class GraphTraversal
    {

        public void BFS(LinkedList<int>[] graph, int startNode)
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
        public void DFS(LinkedList<int>[] graph, int startNode)
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
        public static void bubbleSort(int[] array)
        {
            int n = array.Length;
            int temp;
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
        public static void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(array, low, high);
                QuickSort(array, low, pivot - 1);  // Before pivot
                QuickSort(array, pivot + 1, high); // After pivot
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
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

        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        // Merge Sort
        public static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);
                Merge(array, left, middle, right);
            }
        }

        private static void Merge(int[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] L = new int[n1];
            int[] R = new int[n2];

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

        // Print Array (Helper Method)
        public static void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }

    public class SearchingAlgorithms
    {

    }

    public class LinkedList<T> //  type parameter T
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
            Console.WriteLine($"Added: {data}");
        }

        // Delete the first node with the specified data
        public void Delete(T data)
        {
            if (head == null)
            {
                Console.WriteLine("List is empty.");
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