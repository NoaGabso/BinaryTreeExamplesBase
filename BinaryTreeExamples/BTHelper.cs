using System;
using System.ComponentModel.Design.Serialization;
using System.Reflection.Emit;
//using System.Collections.Generic;
using System.Text;
using DataStructureCore;

namespace BinaryTreeExamples
{
    //כאן יופיעו כל הפעולות העזר עבור עצים
    class BTHelper
    {

        #region יצירת עץ
        public static BinNode<int> CreateTree()
        {
            BinNode<int> root;
            BinNode<int> left;
            BinNode<int> right;
            //קליטת הנתון
            int val = InputData();
            //אם נקלט null
            if (val == -1)
                return null;
            //אחרת צור את השורש
            root = new BinNode<int>(val);
            //צור את תת העץ השמאלי
            left = CreateTree();
            //צור את תת העץ הימני
            right = CreateTree();
            //חבר את תת העץ השמאלי לשורש
            root.SetLeft(left);
            //חבר את תת העץ הימני לשורש
            root.SetRight(right);
            //החזר את שורש תת העץ/שורש העץ
            return root;
        }

        /// <summary>
        /// פעולה היוצרת עץ רנדומלי בגובה
        /// height
        /// ערך כל צומת בטווח שבין max ל min
        /// </summary>
        /// <param name="height"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static BinNode<int> CreateRandomTree(int height, int min, int max)
        {
            Random rnd = new Random();
            BinNode<int> root;
            BinNode<int> left;
            BinNode<int> right;

            if (height == -1)
                return null;
            int val = rnd.Next(min, max + 1);
            root = new BinNode<int>(val);
            left = CreateRandomTree(height - 1, min, max);
            right = CreateRandomTree(height - 1, min, max);
            root.SetLeft(left);
            root.SetRight(right);
            return root;
        }

        private static int InputData()
        {
            Console.WriteLine("Please enter Value, enter -1 for null value (end Branch)");
            return int.Parse(Console.ReadLine());
        }


        #endregion

        #region סריקות
        #region סריקה תחילית
        /// <summary>
        /// פעולה המדפיסה עץ בינארי בסריקה תחילית
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        public static void PrintPreOrder<T>(BinNode<T> root)
        {
            if (root != null)
            {
                Console.WriteLine(root.GetValue());
                PrintPreOrder(root.GetLeft());
                PrintInOrder(root.GetRight());

            }


        }
        #endregion

        #region סריקה תוכית
        /// <summary>
        /// פעולה המדפיסה עץ בינארי בסריקה תוכית
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        public static void PrintInOrder<T>(BinNode<T> root)
        {
            if (root != null)
            {
                PrintPreOrder(root.GetLeft());
                Console.WriteLine(root.GetValue());
                PrintInOrder(root.GetRight());

            }

        }
        #endregion

        #region סריקה סופית
        /// <summary>
        /// פעולה המדפיסה עץ בינארי בסריקה סופית
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        public static void PrintPostOrder<T>(BinNode<T> root)
        {
            if (root != null)
            {
                PrintPreOrder(root.GetLeft());
                PrintInOrder(root.GetRight());
                Console.WriteLine(root.GetValue());

            }

        }
        #endregion
        #endregion

        #region האם עלה
        public static bool IsLeaf<T>(BinNode<T> root)
        {
            //עץ ריק הוא לא עלה
            if (root == null)
                return false;
            //לעלה אין ילד שמאלי ואין ילד ימני. אם אין יוחזר אמת. אחרת יוחזר שקר
            return !root.HasLeft() && !root.HasRight();
        }

        public static bool UpPath(BinNode<int> root)
        {
            if (root == null)
                return false;
            if (IsLeaf(root))
                return true;
            bool left = false;
            bool right = false;
            if (root.HasLeft())
            {
                if (root.GetValue() < root.GetLeft().GetValue())
                    left = true;
            }
            if (root.HasRight())
            {
                if (root.GetValue() < root.GetRight().GetValue())
                    right = true;
            }
            if (left || right)
                return UpPath(root.GetLeft()) || UpPath(root.GetRight());
            return false;
        }

        //פעולה המקבלת עץ בינארי של אותיות קטנות ומעדכנת את הערכים של כל הצמתים להיות האות העוקבת באופן מעגלי
        public static void UpdateLetters(BinNode<char> root)
        {
            if (root != null)
            {
                root.SetValue((char)(((root.GetValue() - 'a' + 1) % 26) + 'a'));

                UpdateLetters(root.GetLeft());
                UpdateLetters(root.GetRight());
            }
        }

        //פעולה גנרית המקבלת עץ ומדפיסה את כל העלים בעץ משמאל לימין
        public static void PrintLeafs<T>(BinNode<T> root)
        {
            if (root != null)
            {
                if (IsLeaf(root))
                    Console.WriteLine(root.GetValue());
                PrintLeafs(root.GetLeft());
                PrintLeafs(root.GetRight());

            }
        }

        //פעולה גנרית המחזירה אמת אם צומת מהווה בן לאב יחיד
        public static bool IsSingleParent<T>(BinNode<T> node)
        {
            if (node == null)
                return false;
            return ((node.HasLeft() && !node.HasRight()) || (node.HasRight() && !node.HasLeft()));
        }
        //פעולה המחזירה את מספר הבנים היחידים בעץ
        public static int CountSingleParents<T>(BinNode<T> root)
        {
            if (root == null)
                return 0;
            if (IsSingleParent(root))
                return 1 + CountSingleParents(root.GetLeft()) + CountSingleParents(root.GetRight());
            return CountSingleParents(root.GetLeft()) + CountSingleParents(root.GetRight());

        }
        //פעולה המקבלת עץ בינארי של מספרים שלמים ומחזירה את מספר הבנים היחידים שיש להם בן יחיד
        public static int CountSingleChild(BinNode<int> root)
        {
            if (root == null)
                return 0;
            if (IsSingleParent(root) && IsSingleParent(root.GetLeft()) || IsSingleParent(root) && IsSingleParent(root.GetRight()))
                return 1 + CountSingleChild(root.GetLeft()) + CountSingleChild(root.GetRight());
            return CountSingleChild(root.GetLeft()) + CountSingleChild(root.GetRight());
        }
        //הגובה של העץ
        public static int BinTreeHight<T>(BinNode<T> root)
        {
            if (root == null)
                return 0;
            if ((IsSingleParent(root) && IsSingleParent(root.GetLeft()) || IsSingleParent(root) && IsSingleParent(root.GetRight())))
                return 1 + CountSingleParents(root.GetLeft()) + CountSingleParents(root.GetRight());
            return CountSingleParents(root.GetLeft()) + CountSingleParents(root.GetRight());
        }
        #region פעולות על עצים
        #region ספירת צמתים
        /// <summary>
        /// פעולה המקבלת שורש של עץ ומחזירה את כמות הצמתים בעץ
        /// n- מספר הצמתים
        /// הפעולה מבקרת בכל צומת בדיוק פעם אחת
        /// ולכן O(n)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int CountTreeNodes<T>(BinNode<T> root)
        {
            if (root == null)
                return 0;
            return 1 + CountTreeNodes(root.GetLeft()) + CountTreeNodes(root.GetRight());
        }
        #endregion

        #region האם ערך קיים בעץ
        public static bool IsExistsInTree<T>(BinNode<T> root, T val)
        {
            if (root == null)
                return false;

            //if (root.GetValue().Equals(val))
            //    return true;
            //return IsExistsInTree(root.GetLeft(), val) || IsExistsInTree(root.GetRight(), val);
            //או בשורש או בצד שמאל או בצד ימין
            return (root.GetValue().Equals(val)) || IsExistsInTree(root.GetLeft(), val) || IsExistsInTree(root.GetRight(), val);

        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool EachHasTwoChilds<T>(BinNode<T> root)
        {
            if (root == null)
                return true;
            //אם יש ילד יחיד שמאלי
            if (root.HasLeft() && !root.HasRight())
                return false;
            //אם יש ילד יחיד ימני
            if (root.HasRight() && !root.HasLeft())
                return false;
            //אם הצומת הנוכחי מקיים את התנאים נבדוק שכל תת עץ שמאל מקיים וגם תת עץ ימין
            return EachHasTwoChilds(root.GetLeft()) && EachHasTwoChilds(root.GetRight());


        }

        public static void BreadthSearch<T>(BinNode<T> root)
        {
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            BinNode<T> node;
            queue.Insert(root);
            while (!queue.IsEmpty())
            {
                node = queue.Remove();
                Console.WriteLine(node.GetValue());
                if (node.HasLeft())
                    queue.Insert(node.GetLeft());
                if (node.HasRight())
                    queue.Insert(node.GetRight());

            }
        }
        #endregion

        public static int MaxBreadthSearch(BinNode<int> root)
        {
            int max = root.GetValue();
            Queue<BinNode<int>> queue = new Queue<BinNode<int>>();
            BinNode<int> node;
            queue.Insert(root);
            while (!queue.IsEmpty())
            {
                node = queue.Remove();
                //Console.WriteLine(node.GetValue());
                if (node.GetValue() > max)
                    max = node.GetValue();
                if (node.HasLeft())
                    queue.Insert(node.GetLeft());
                if (node.HasRight())
                    queue.Insert(node.GetRight());

            }
            return max;
        }

        public static int MinBreadthSearch(BinNode<int> root)
        {
            int min = root.GetValue();
            Queue<BinNode<int>> queue = new Queue<BinNode<int>>();
            BinNode<int> node;
            queue.Insert(root);
            while (!queue.IsEmpty())
            {
                node = queue.Remove();
                //Console.WriteLine(node.GetValue());
                if (node.GetValue() < min)
                    min = node.GetValue();
                if (node.HasLeft())
                    queue.Insert(node.GetLeft());
                if (node.HasRight())
                    queue.Insert(node.GetRight());

            }
            return min;
        }

        public static int WhichLevel<T>(BinNode<T> root, T x)
        {
            if (root == null)
                return -1;
            int level = 0;
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            BinNode<T> node;
            Queue<int> levels = new Queue<int>();
            queue.Insert(root);
            levels.Insert(level);
            while (!queue.IsEmpty())
            {
                node = queue.Remove();
                //נשלוף את הרמה של הצומת
                level = levels.Remove();
                if (node.GetValue().Equals(x))
                    return level;
                if (node.HasLeft())
                {
                    queue.Insert(node.GetLeft());
                    //נכניס את הרמה הבאה
                    levels.Insert(level + 1);
                }
                if (node.HasRight())
                {
                    queue.Insert(node.GetRight());
                    levels.Insert(level + 1);
                }


            }
            return -1;


        }

        public static int DiffrenceBetweenLeven(BinNode<int> root, int x, int y)
        {
            x = WhichLevel(root, x);
            y = WhichLevel(root, y);
            return x - y;
        }
        public static void PrintInLevel<T>(BinNode<T> root, T x)
        {
            if (root == null)
                Console.WriteLine("null");
            int level = 0;
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            BinNode<T> node;
            Queue<int> levels = new Queue<int>();
            queue.Insert(root);
            levels.Insert(level);
            while (!queue.IsEmpty())
            {
                node = queue.Remove();
                //נשלוף את הרמה של הצומת
                level = levels.Remove();
                if (level.Equals(x))
                    Console.WriteLine(node.GetValue());
                if (node.HasLeft())
                {
                    queue.Insert(node.GetLeft());
                    //נכניס את הרמה הבאה
                    levels.Insert(level + 1);
                }
                if (node.HasRight())
                {
                    queue.Insert(node.GetRight());
                    levels.Insert(level + 1);
                }
            }
        }
        public static void PrintEvenLevel<T>(BinNode<T> root)
        {
            if (root == null)
                Console.WriteLine("null");
            int level = 0;
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            BinNode<T> node;
            Queue<int> levels = new Queue<int>();
            queue.Insert(root);
            levels.Insert(level);
            while (!queue.IsEmpty())
            {
                node = queue.Remove();
                //נשלוף את הרמה של הצומת
                level = levels.Remove();
                if (level % 2 == 0)
                    Console.WriteLine(node.GetValue());
                if (node.HasRight())
                {
                    queue.Insert(node.GetRight());
                    levels.Insert(level + 1);
                }
                if (node.HasLeft())
                {
                    queue.Insert(node.GetLeft());
                    //נכניס את הרמה הבאה
                    levels.Insert(level + 1);
                }

            }
        }

        public static int WhichWidth(BinNode<int> root)
        {
            if (root == null)
                Console.WriteLine("null");
            int count = 0;
            Queue<BinNode<int>> queue = new Queue<BinNode<int>>();
            BinNode<int> node;
            Queue<int> counts1 = new Queue<int>();
            Queue<int> counts2 = new Queue<int>();
            queue.Insert(root);
            counts1.Insert(count);
            while (!queue.IsEmpty())
            {

                node = queue.Remove();
                count = counts1.Remove();
                counts2.Insert(count);

                if (node.HasLeft())
                {
                    queue.Insert(node.GetLeft());
                    //נכניס את הרמה הבאה
                    counts1.Insert(count + 1);
                }
                if (node.HasRight())
                {
                    queue.Insert(node.GetRight());
                    counts1.Insert(count + 1);
                }
            }
            int max = 0;
            int counter = 0;

            int m = BinTreeHight(root);
            Queue<int> countsmax = new Queue<int>();
            for (int i = 0; i <= m; i++)
            { while (!counts2.IsEmpty())
                {
                    counter = 0;
                    int x = counts2.Remove();
                    if (x == i)
                    { counter++; }
                    countsmax.Insert(x);
                }
                if (max < counter)
                    max = counter;

                while (!countsmax.IsEmpty())
                {
                    counts2.Insert(countsmax.Remove());
                }

            }
            return max;
        }
        /// <summary>
        /// עמ 176 שאלה 9 מהספר
        /// </summary>
        /// <param name="root"></param>
        public static void UpdateCharTree(BinNode<char> root)
        {

            if (root != null)
            {
                char ch = root.GetValue();
                //האם אותיות גדולות?
                if (ch >= 'a' && ch <= 'z')
                    //convert the char to range of 0-25  letters (ch-'a')==>add 1===>make sure it is still between 0-25 (%26)===> convert it to a char (+'a')==>covert back to char (char)(<result>)
                    ch = (char)(((ch - 'a') + 1) % 26 + 'a');
                //convert the char to range of 0-25  letters (ch-'A')==>add 1===>make sure it is still between 0-25 (%26)===> convert it to a char (+'A')==>covert back to char (char)(<result>)
                if (ch >= 'A' && ch <= 'Z')
                    ch = (char)(((ch - 'A') + 1) % 26 + 'A');
                //עדכן את הערך החדש
                root.SetValue(ch);
                //עשו זאת עבור תת עץ שמאל ותת עץ ימין
                UpdateCharTree(root.GetLeft());
                UpdateCharTree(root.GetRight());
            }


        }

        /// <summary>
        /// תרגיל 14
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>

        public static int CountLeaves<T>(BinNode<T> root)
        {
            return 0;

        }
        /// <summary>
        /// שאלה 12
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int CountBiggerInBetween(BinNode<double> root)
        {
            return 0;
        }

        #endregion

        #region גובה עץ
        /// <summary>
        /// פעולה המחשבת את גובה העץ בסריקה סופית
        /// גובה תת עץ שמאל, גובה תת עץ ימין ואז הגובה של העץ כולו הינו
        /// 1+ גובה המקסימלי מבין שני תתי העצים
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>

        #endregion

        #region הדפסת רמה בעץ

        #region פעולת המעטפת
        /// <summary>
        /// הדפסת הצמתים ברמה מסוימת בעץ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="targetLevel"></param>
        public static void PrintNodesInLevel<T>(BinNode<T> root, int targetLevel)
        {

        }
        #endregion

        #region פעולת ההדפסה
        /// <summary>
        /// הפעולה תדפיס את ערך הצומת ברמה המבוקשת
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="targetLevel">הרמה שנרצה להדפיס</param>
        /// <param name="currentLevel">הרמה הנוכחית שאליה הגענו בסריקה</param>
        private static void PrintNodesInLevel<T>(BinNode<T> root, int targetLevel, int currentLevel)
        {


        }
        #endregion
        #endregion

        #region חישוב רוחב של עץ

        #region פעולת ראשית
        /// <summary>
        /// פעולה מחשבת את רוחב העץ- הרמה שמכילה את הכי הרבה צמתים
        /// הפעולה תחזיר מערך בגודל 2 - בתא 0 יוחזר הרמה שמכילה את הכי הרבה צמתים
        /// ובתא 1 - הרוחב של העץ
        /// נגדיר h - גובה של העץ
        /// נגדיר n- כמות הצמתים בעץ
        /// CountNodesInLevel -  O(n)
        /// הפעולה מחשבת גובה ואז רצה בלולאה כגובה העץ ולכן נקבל
        /// 
        /// o(h)+(o(h) * O(n)).
        /// ==>(o(h) * O(n))
        /// במקרה הגרוע זה עץ שרשרת (כמו שרשרת חוליות רגילה)
        /// במקרה כזה 
        /// h==n
        /// ולכן
        /// O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int[] BinTreeWidth<T>(BinNode<T> root)
        {


            return null;


        }
        #endregion


        #region פעולות עזר
        /// <summary>
        /// פעולת מעטפת המקבלת רמה בעץ ומחזירה כמה צמתים יש באותה רמה
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="currentTreeLevel"></param>
        /// <returns></returns>
        private static int CountNodesInLevel<T>(BinNode<T> root, int currentTreeLevel)
        {
            return 0;
        }
        private static int CountNodesInLevel<T>(BinNode<T> root, int targetTreeLevel, int currentLevel)
        {
            return 0;

        }
        #endregion

        #endregion

        #region חישוב רוחב של עץ באמצעות מערך מונים
        /// <summary>
        /// n= כמות הצמתים
        /// CountNodesInLevel- מבצעת סריקה אחת על העץ ולכן O(n)
        /// FindMax- במקרה של עץ שרשרת (כמו שרשרת חוליות)- O(n)
        /// ולכן קיבלנו
        /// O(n) + O(n)=O(n)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>


        #region פעולות עזר
        /// <summary>
        /// הפעולה מקבלת שורש עץ
        /// מערך מונים בגודל גובה העץ
        /// ורמה נוכחית
        /// הפעולה תוסיף 1 בתא המייצג את מספר הרמה בתהליך הסריקה
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="treeLevels"></param>
        /// <param name="currentLevel"></param>
        private static void CountNodesInLevel<T>(BinNode<T> root, int[] treeLevels, int currentLevel)
        {

        }

        /// <summary>
        /// מציאת מקסימום במערך.
        /// הפעולה מחזירה מערך בגודל 2
        /// תא הראשון המיקום של הערך המקסימלי
        /// והתא השני הערך המקסימלי במערך
        /// </summary>
        /// <param name="treeLevels"></param>
        /// <returns></returns>
        private static int[] FindMax(int[] treeLevels)
        {
            return null;
        }
        #endregion
        #endregion

        #region Binary Search Tree

        #region הוספת ערך לעץ חיפוש
        public static BinNode<int> AddToBST(BinNode<int> t, int x)
        {
            if (t == null)
                return new BinNode<int>(x);
            if (t.GetValue() > x)
                t.SetLeft(AddToBST(t.GetLeft(), x));
            else
                t.SetRight(AddToBST(t.GetRight(), x));
            return t;
        }
        #endregion

        #region מקסימום/מינימום בעץ חיפוש
        public static int FindMaxInBST(BinNode<int> t)
        {
            return 0;
        }
        #endregion

        #region האם עץ חיפוש

        /// <summary>
        /// כל צומת צריך להיות גדול יותר מהכי גדול בתת העץ השמאלי שלו
        /// ויותר קטן מהכי קטן בתת עץ הימני שלו
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsBST(BinNode<int> t)
        {
            if (t == null)
                return true;
            if (t.HasLeft())
            {
                if (t.GetValue() <= MaxBreadthSearch(t.GetLeft()))
                    return false;
            }
            if (t.HasRight())
            {
                if (t.GetValue() > MinBreadthSearch(t.GetRight()))
                    return false;
            }
            return IsBST(t.GetLeft()) && IsBST(t.GetRight());

        }

        public static int FindMin(BinNode<int> t)
        {
            return 0;
        }


        public static int FindMax(BinNode<int> t)
        {
            return 0;
        }
        #endregion

        #region מצא הורה בעץ חיפוש
        public static BinNode<int> FindParent(BinNode<int> root, BinNode<int> child)
        {

            return null;
        }
        #endregion

        #region מחיקת ערך בעץ חיפוש
        //            The node has no children(it's a leaf node). You can delete it. ...
        //The node has just one child.To delete the node, replace it with that child. ...
        //The node has two children.In this case, find the MAX in the LEFT Side of the node. (or MIN of the RIGHT SIDE OF THE NODE)

        public static BinNode<int> RemoveFromBST(BinNode<int> root, int key)
        {



            return root;
        }





        #endregion
        #endregion

        public static bool IsZigzag<T>(BinNode<T> root)
            {
            if(root==null|| IsLeaf(root)) return false;
            return IsZigzag(root.GetLeft(), true) && IsZigzag(root.GetRight(), false);
            }
        public static bool IsZigzag<T>(BinNode<T> root, bool isleft)
        {
            if(root==null|| IsLeaf(root)) return true;
            if(isleft && !root.HasRight())
                return false;
            else if(!isleft && !root.HasLeft()) return false;
            return IsZigzag(root.GetLeft(), true) && IsZigzag(root.GetRight(), false);
        }

} }
