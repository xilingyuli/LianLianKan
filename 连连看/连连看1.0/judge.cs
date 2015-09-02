using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连连看1._0
{
    public class judge
    {
        private int x1, y1, x2, y2, n, m;
        private int[,] a, key;
        public judge(int x1, int y1, int x2, int y2, int n, int m, int[,] key)
        {
            this.key = key;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.n = n;
            this.m = m;
        }
        public bool Judge()
        {
            if (key[x1,y1] != key[x2,y2])
                return false;
            else if (Judge0())
                return true;
            else if (Judge1())
                return true;
            else if (Judge2())
                return true;
            else
                return false;
        }
        private bool Judge0()
        {
            bool find = true;
            if (x1 == x2)
            {
                if (y1 < y2)
                    for (int i = 1; i < y2 - y1; i++)
                    {
                        if (key[x1,y1 + i] != 0)
                        {
                            find = false;
                            break;
                        }
                    }
                else
                    for (int i = 1; i < y1 - y2; i++)
                    {
                        if (key[x2,y2 + i] != 0)
                        {
                            find = false;
                            break;
                        }
                    }
            }
            else if (y1 == y2)
            {
                if (x1 < x2)
                    for (int i = 1; i < x2 - x1; i++)
                    {
                        if (key[x1 + i,y1] != 0)
                        {
                            find = false;
                            break;
                        }
                    }
                else
                    for (int i = 1; i < x1 - x2; i++)
                    {
                        if (key[x2 + i,y2] != 0)
                        {
                            find = false;
                            break;
                        }
                    }
            }
            else
                find = false;
            return find;
        }
        private bool Judge1()
        {
            a = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    this.a[i,j] = key[i,j];
            for (int i = 1; ; i++)
                if (x1 - i >= 0 && a[x1 - i,y1] == 0)
                    a[x1 - i,y1] = -1;
                else
                    break;
            for (int i = 1; ; i++)
                if (x1 + i < n && a[x1 + i,y1] == 0)
                    a[x1 + i,y1] = -1;
                else
                    break;
            for (int i = 1; ; i++)
                if (x2 - i >= 0 && a[x2 - i,y2] == 0)
                    a[x2 - i,y2] = -1;
                else
                    break;
            for (int i = 1; ; i++)
                if (x2 + i < n && a[x2 + i,y2] == 0)
                    a[x2 + i,y2] = -1;
                else
                    break;
            for (int i = 1; ; i++)
                if (y1 - i >= 0 && a[x1,y1 - i] == 0)
                    a[x1,y1 - i] = -2;
                else if (y1 - i >= 0 && a[x1,y1 - i] == -1)
                    return true;
                else
                    break;
            for (int i = 1; ; i++)
                if (y1 + i < m && a[x1,y1 + i] == 0)
                    a[x1,y1 + i] = -2;
                else if (y1 + i < m && a[x1,y1 + i] == -1)
                    return true;
                else
                    break;
            for (int i = 1; ; i++)
                if (y2 - i >= 0 && a[x2,y2 - i] == 0)
                    a[x2,y2 - i] = -2;
                else if (y2 - i >= 0 && a[x2,y2 - i] == -1)
                    return true;
                else
                    break;
            for (int i = 1; ; i++)
                if (y2 + i < m && a[x2,y2 + i] == 0)
                    a[x2,y2 + i] = -2;
                else if (y2 + i < m && a[x2,y2 + i] == -1)
                    return true;
                else
                    break;
            return false;
        }
        private bool Judge2()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m - 1; j++)
                {
                    if (a[i,j] == -1)
                    {
                        j++;
                        while (a[i,j] == 0 && j < m - 1)
                            j++;
                        if (a[i,j] == -1)
                            return true;
                    }
                }
            for (int j = 0; j < m; j++)
                for (int i = 0; i < n - 1; i++)
                {
                    if (a[i,j] == -2)
                    {
                        i++;
                        while (a[i,j] == 0 && i < n - 1)
                            i++;
                        if (a[i,j] == -2)
                            return true;
                    }
                }
            return false;
        }
    }

}
