# Spreadsheet

Simple Spreadsheet

# Use Case Examples

1) create a 10x5 spreadsheet
enter command:C 10 5
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------

----------------------------------------

----------------------------------------

----------------------------------------

----------------------------------------

2) update cell [1,1] to 2
enter command:N 1 1 2
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------
    2
----------------------------------------

----------------------------------------

----------------------------------------

----------------------------------------

3) update cell [2,3] to 10 
enter command:N 2 3 10
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------
    2
----------------------------------------

----------------------------------------
        10
----------------------------------------

----------------------------------------

4) update cell [4,2] to 5
enter command:N 4 2 5
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------
    2
----------------------------------------
                5
----------------------------------------
        10
----------------------------------------

----------------------------------------

5) calculate sum ranged from [1,1] to [4,3], and update to [0,0]
enter command:S 1 1 4 2 0 0
0   1   2   3   4   5   6   7   8   9
========================================
7
----------------------------------------
    2
----------------------------------------
                5
----------------------------------------
        10
----------------------------------------

----------------------------------------