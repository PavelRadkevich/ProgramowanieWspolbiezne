using Dane;

namespace Logika
{
    public static class BinarySearchTree
    {
        public static void Insert(Node node, Ball ball)
        {
            if (ball.x < node.ball.x)
            {
                if (node.left == null)
                {
                    node.left = new Node
                    {
                        ball = ball
                    };
                }
                else
                {
                    Insert(node.left, ball);
                }
            }
            else
            {
                if (node.right == null)
                {
                    node.right = new Node
                    {
                        ball = ball
                    };
                }
                else
                {
                    Insert(node.right, ball);
                }
            }
        }
    }
}
