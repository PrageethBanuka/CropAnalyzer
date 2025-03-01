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
                    while(neighbor != null)//traverse through linked list
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
