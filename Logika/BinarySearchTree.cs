using Dane;
using System.Device.Location;
using System.Drawing;
using System.Windows.Forms;

namespace Logika
{
    public static class BinarySearchTree
    {
        public static void InsertX(Node node, Ball ball)
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
                    InsertX(node.left, ball);
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
                    InsertX(node.right, ball);
                }
            }
        }
        public static void InsertY(Node node, Ball ball)
        {
            if (ball.y < node.ball.y)
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
                    InsertY(node.left, ball);
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
                    InsertY(node.right, ball);
                }
            }
        }
    }
}
