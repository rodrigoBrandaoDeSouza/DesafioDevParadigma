namespace DesafioDevParadigma // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Tuple<string, string>[] input = {
            Tuple.Create("A", "B"),
            Tuple.Create("A", "C"),
            Tuple.Create("B", "G"),
            Tuple.Create("C", "H"),
            Tuple.Create("E", "F"),
            Tuple.Create("B", "D"),
            Tuple.Create("C", "E"),
        };

            TreeBuilder builder = new TreeBuilder();
            Node root = builder.BuildTree(input);

            PrintTreeIdentado(root);
        }


        static void PrintTreeIdentado(Node root, string prefix = "")
        {
            Console.WriteLine(prefix + root.Value);
            for (int i = 0; i < root.Children.Count; i++)
            {
                Node child = root.Children[i];
                string newPrefix = prefix;
                if (i < root.Children.Count - 1)
                {
                    newPrefix += "├── ";
                }
                else
                {
                    newPrefix += "└── ";
                }
                PrintTreeIdentado(child, newPrefix);
            }
        }

        public class Node
        {
            public string Value { get; set; }
            public List<Node> Children { get; set; }

            public Node(string value)
            {
                Value = value;
                Children = new List<Node>();
            }
        }

        public class TreeBuilder
        {
            public Node BuildTree(Tuple<string, string>[] input)
            {
                var nodes = new Dictionary<string, Node>();
                // Cria nós unicos para serem os pais
                foreach (var pair in input)
                {
                    if (!nodes.ContainsKey(pair.Item1))
                        nodes[pair.Item1] = new Node(pair.Item1);
                    if (!nodes.ContainsKey(pair.Item2))
                        nodes[pair.Item2] = new Node(pair.Item2);
                }

                // Define os filhos de cada pai
                foreach (var pair in input)
                {
                    var parent = nodes[pair.Item1];
                    var child = nodes[pair.Item2];
                    if (parent.Children.Count > 1)
                        throw new Exception("E1: Mais de 2 filhos");
                    if (child.Children.Contains(parent))
                        throw new Exception("E2: Ciclo Presente");
                    parent.Children.Add(child);
                }

                // Encontra a raiz (o pai que não tem pai)
                var root = nodes.Values.FirstOrDefault(n => !input.Select(p => p.Item2).Contains(n.Value));
                if (root == null)
                    throw new Exception("E3: Raízes múltiplas");

                return root;
            }
        }
    }
}