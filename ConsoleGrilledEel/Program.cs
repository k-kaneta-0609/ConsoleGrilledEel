using System;
using System.Collections.Generic;

namespace ConsoleGrilledEel
{
    class Program
    {
        class DirectionInfo { public char Direction; public bool Reviewed; }

        static void Main(string[] args)
        {
            // 初期情報の格納
            string[] firstLine = Console.ReadLine().Split(' ');
            int maxBlockNo = int.Parse(firstLine[0]) * 2;
            int remains = int.Parse(firstLine[0]);
            List<int> moveList = new();
            foreach (int _ in System.Linq.Enumerable.Range(0, int.Parse(firstLine[1])))
            {
                moveList.Add(int.Parse(Console.ReadLine()));
            }

            // 方向決定
            const char AdditionDirection = 'R';
            const char SubtractionDirection = 'L';
            List<DirectionInfo> directionList = new();
            int step = 0;
            while (step < moveList.Count)
            {
                if (0.Equals(step))
                {
                    // 最初の移動
                    directionList.Add(new DirectionInfo() { Direction = AdditionDirection, Reviewed = false });
                    remains += moveList[step];
                    step++;
                }
                else
                {
                    // 2回目以降の移動
                    if ((remains < moveList[step]) && (maxBlockNo < remains + moveList[step]))
                    {
                        // 移動できない → 1つ前の移動方向の見直しへ
                        if (AdditionDirection.Equals(directionList[step - 1].Direction))
                        {
                            remains -= moveList[step - 1];
                        }
                        else
                        {
                            remains += moveList[step - 1];
                        }
                        step--;
                    }
                    else
                    {
                        // 移動可能
                        if (step < directionList.Count)
                        {
                            // 既に1度移動方向を決めていた
                            if (directionList[step].Reviewed)
                            {
                                // 既に1度決めた移動方向を見直している → 1つ前の移動方向の見直しへ
                                if (AdditionDirection.Equals(directionList[step - 1].Direction))
                                {
                                    remains -= moveList[step - 1];
                                }
                                else
                                {
                                    remains += moveList[step - 1];
                                }
                                step--;
                            }
                            else
                            {
                                // 移動方向の見直しをしたことがないので、方向転換を試みる
                                if (AdditionDirection.Equals(directionList[step].Direction))
                                {
                                    // 左方向へ変更を試みる
                                    directionList[step].Reviewed = true;
                                    if (remains < moveList[step])
                                    {
                                        // 左方向へ変更不可 → 1つ前の移動方向の見直しへ
                                        if (AdditionDirection.Equals(directionList[step - 1].Direction))
                                        {
                                            remains -= moveList[step - 1];
                                        }
                                        else
                                        {
                                            remains += moveList[step - 1];
                                        }
                                        step--;
                                    }
                                    else
                                    {
                                        // 左方向へ変更可能 → 方向転換し次へ
                                        directionList[step].Direction = SubtractionDirection;
                                        for (int i = directionList.Count - 1; step < i; i--) directionList.RemoveAt(i);
                                        remains -= moveList[step];
                                        step++;
                                    }
                                }
                                else
                                {
                                    // 右方向へ変更を試みる
                                    directionList[step].Reviewed = true;
                                    if (maxBlockNo < remains + moveList[step])
                                    {
                                        // 右方向へ変更不可 → 1つ前の移動方向の見直しへ
                                        if (AdditionDirection.Equals(directionList[step - 1].Direction))
                                        {
                                            remains -= moveList[step - 1];
                                        }
                                        else
                                        {
                                            remains += moveList[step - 1];
                                        }
                                        step--;
                                    }
                                    else
                                    {
                                        // 右方向へ変更可能 → 方向転換し次へ
                                        directionList[step].Direction = AdditionDirection;
                                        for (int i = directionList.Count - 1; step < i; i--) directionList.RemoveAt(i);
                                        remains += moveList[step];
                                        step++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // 初めての移動方向の決定
                            if ((remains < moveList[step]) && (maxBlockNo < remains + moveList[step]))
                            {
                                // 移動できない → 1つ前の移動方向の見直しへ
                                if (AdditionDirection.Equals(directionList[step - 1].Direction))
                                {
                                    remains -= moveList[step - 1];
                                }
                                else
                                {
                                    remains += moveList[step - 1];
                                }
                                step--;
                            }
                            else if (remains + moveList[step] <= maxBlockNo)
                            {
                                // 右方向へ移動可能 → 右方向へ移動し次へ
                                directionList.Add(new DirectionInfo() { Direction = AdditionDirection, Reviewed = false });
                                remains += moveList[step];
                                step++;
                            }
                            else
                            {
                                // 左方向へ移動可能 → 左方向へ移動し次へ
                                directionList.Add(new DirectionInfo() { Direction = SubtractionDirection, Reviewed = false });
                                remains -= moveList[step];
                                step++;
                            }
                        }
                    }
                }
            }

            // 結果出力
            System.Text.StringBuilder result = new();
            directionList.ForEach(info => result.Append(info.Direction)); 
            Console.WriteLine(result.ToString());
        }
    }
}
