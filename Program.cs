using System;

class Program
{
    static void Main()
    {
        // Graph represented as an adjacency list using LinkedLists
        LinkedList<int>[] graph = new LinkedList<int>[]
        {
            new LinkedList<int>(new int[] { 1, 2 }),    // Neighbors of node 0
            new LinkedList<int>(new int[] { 0, 3, 4 }), // Neighbors of node 1
            new LinkedList<int>(new int[] { 0, 5, 6 }), // Neighbors of node 2
            new LinkedList<int>(new int[] { 1,5,8 }),       // Neighbors of node 3
            new LinkedList<int>(new int[] { 1 }),       // Neighbors of node 4
            new LinkedList<int>(new int[] { 5,4 }),       // Neighbors of node 5
            new LinkedList<int>(new int[] { 2,0 }),        // Neighbors of node 6
            new LinkedList<int>(new int[] { 2,7,4 }),
            new LinkedList<int>(new int[] { 2,4,3 })
        };

        int startNode = 0;
        Console.WriteLine("BFS Traversal:");
        BFS(graph, startNode);
       
        //uncomment this and comment above, if want to test DSF
        /* 
        int startNode = 0;
        Console.WriteLine("DFS Traversal:");
        DFS(graph, startNode);*/
    }

    static void BFS(LinkedList<int>[] graph, int startNode)
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
            foreach (var neighbor in graph[current])
            {
                if (!visited[neighbor])
                {
                    // Enqueue unvisited neighbor
                    queue[rear] = neighbor;
                    rear++;
                    visited[neighbor] = true;
                }
            }
        }
    }



    static void DFS(LinkedList<int>[] graph, int startNode)
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
                foreach (var neighbor in graph[current])
                {
                    if (!visited[neighbor])
                    {
                        top++;
                        stack[top] = neighbor;
                    }
                }
            }
        }
    }
}
