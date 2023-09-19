using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{

    [SerializeField]
    private GameObject _card;

    [SerializeField]
    private GameManager _gameManager;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void update()
    //{
        
    //}

    public void OnClick()
    {
        string tmpFormula = "";
        double result = 0.0;

        foreach (Transform child in _card.transform)
        {
            tmpFormula += child.GetComponent<Card>().cardName;
        }

        //Debug.Log($"tmpFormula : {tmpFormula}");

        List<char> list = new(tmpFormula);
        char[] c = list.ToArray();

        // 構文解析
        Node node = Parse(c);

        // 計算
        try
        {
            result = Eval(node);
        }
        catch (System.Exception e)
        {
            Debug.Log($"計算できません 理由:{e.Message}");
            return;
        }

        Debug.Log(tmpFormula + " = " + result);

        _gameManager.GetComponent<GameManager>().AttackToEnemy((int)result);

        GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("Attack1");

        // カードを消す
        foreach (Transform child in _card.transform)
        {
            Destroy(child.gameObject);
        }
    }


    // ----------------------------------------

    /// <summary>
    /// cが数や内部処理で使用している文字かどうかの判定
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static bool IsNumber(char c)
    {
        return char.IsDigit(c) || c == 'x' || c == 'X' || c == '#';
    }

    
    /// <summary>
    /// ノードごとの数式を計算
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static double Eval(Node node)
    {
        List<string> ns; // 数
        List<char> ope; // 演算子

        // 字句解析
        LexicalAnalysis(node.formula, out ns, out ope);

        if (ope.Count == 0)
        {
            throw new System.Exception("何かしらの演算子が入力されていないため");
        }

        // nsを元に数字を決定
        var numbers = new List<double>();
        {
            int child = 0;
            for (int i = 0; i < ns.Count; i++)
            {
                double num;
                switch (ns[i])
                {
                    case "#":
                        num = Eval(node.childs[child++]);
                        break;
                    default:
                        double.TryParse(ns[i], out num);
                        break;
                }
                numbers.Add(num);
            }
        }

        // 乗算除算処理
        {
            for (int i = 0; i < ope.Count;)
            {
                switch (ope[i])
                {
                    case '*':
                        {
                            double left = numbers[i];
                            double right = numbers[i + 1];
                            numbers[i] = left * right;
                            numbers.RemoveAt(i + 1);
                            ope.RemoveAt(i);
                        }
                        break;

                    case '/':
                        {
                            double left = numbers[i];
                            double right = numbers[i + 1];
                            numbers[i] = left / right;
                            numbers.RemoveAt(i + 1);
                            ope.RemoveAt(i);
                        }
                        break;

                    default:
                        i++;
                        break;
                }
            }
        }

        // 加算減算処理
        double total = numbers[0];
        {
            for (int i = 0; i < ope.Count; i++)
            {
                switch (ope[i])
                {
                    case '+':
                        total += numbers[i + 1];
                        break;
                    case '-':
                        total -= numbers[i + 1];
                        break;
                }
            }
        }

        if (double.IsInfinity(total))
        {
            throw new System.Exception("不正な計算結果");
        }

        return total;
    }


    /// <summary>
    /// 字句解析
    /// </summary>
    /// <param name="str"></param>
    /// <param name="ns"></param>
    /// <param name="os"></param>
    private static void LexicalAnalysis(string str, out List<string> ns, out List<char> os)
    {
        ns = new List<string>();
        os = new List<char>();

        string text = "";
        for (int i = 0; i < str.Length; i++)
        {
            switch (str[i])
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    ns.Add(text);
                    os.Add(str[i]);
                    text = "";
                    break;
                default:
                    if (IsNumber(str[i]))
                    {
                        text += str[i];
                        if (i == str.Length - 1)
                        {
                            ns.Add(text);
                            text = "";
                        }
                    }
                    break;
            }
        }

    }


    /// <summary>
    /// 数式の構文解析
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static Node Parse(char[] c)
    {
        Node root = new Node();
        Node target = root;

        for (int i = 0; i < c.Length; i++)
        {
            switch (c[i])
            {
                case '(':
                    {
                        target.formula += "#";

                        // 子ノードを追加
                        Node node = new Node();
                        target.Add(node);
                        target = node;
                    }
                    break;
                case ')':
                    {
                        target = target.parent;
                    }
                    break;
                default:
                    target.formula += c[i];
                    break;
            }
        }

        return root;
    }

    public class Node
    {
        // 数式
        public string formula = "";

        // 子ノード
        public List<Node> childs = new();

        // 親ノード
        public Node parent { get; private set; }

        public void Add(Node node)
        {
            node.parent = this;
            childs.Add(node);
        }

    }

}
