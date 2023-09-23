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

    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        // �\�����
        Node node = Parse(c);

        // �v�Z
        try
        {
            result = Eval(node);
        }
        catch (System.Exception e)
        {
            Debug.Log($"�v�Z�ł��܂��� ���R:{e.Message}");
            return;
        }

        Debug.Log(tmpFormula + " = " + result);

        _gameManager.GetComponent<GameManager>().AttackToEnemy((int)result);

        GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("Attack1");
        audioSource.PlayOneShot(sound1);
        // �J�[�h������
        foreach (Transform child in _card.transform)
        {
            Destroy(child.gameObject);
        }
    }


    // ----------------------------------------

    /// <summary>
    /// c��������������Ŏg�p���Ă��镶�����ǂ����̔���
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static bool IsNumber(char c)
    {
        return char.IsDigit(c) || c == 'x' || c == 'X' || c == '#';
    }

    
    /// <summary>
    /// �m�[�h���Ƃ̐������v�Z
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static double Eval(Node node)
    {
        List<string> ns; // ��
        List<char> ope; // ���Z�q

        // ������
        LexicalAnalysis(node.formula, out ns, out ope);

        if (ope.Count == 0)
        {
            throw new System.Exception("��������̉��Z�q�����͂���Ă��Ȃ�����");
        }

        // ns�����ɐ���������
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

        // ��Z���Z����
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

        // ���Z���Z����
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
            throw new System.Exception("�s���Ȍv�Z����");
        }

        return total;
    }


    /// <summary>
    /// ������
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
    /// �����̍\�����
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

                        // �q�m�[�h��ǉ�
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
        // ����
        public string formula = "";

        // �q�m�[�h
        public List<Node> childs = new();

        // �e�m�[�h
        public Node parent { get; private set; }

        public void Add(Node node)
        {
            node.parent = this;
            childs.Add(node);
        }

    }

}
