# DU-CLI-Calculator

Calculation CLI Tool for Dual Universe

## Commands

```
./DUCalculator.exe path "<filePath>" "<Position>"
```
Order the positions in an AR File to make a path starting from shortest distance

```
./DUCalculator.exe directions all 100000 "<Position>"
```
Calculates Positions on all directions adding 100km (each unit is a meter) to a given position

Reads an AR file from `filePath` and outputs an ordered list of positions for the optimal path navigation.

### Output Example:

```
BA_A_EMO_615_0121 = '::pos{0,0,-4868468,-2704013.8,1144904.5}',
BA_A_CMD_869_0121 = '::pos{0,0,-6151337.5,-4122430,-2278690}',
EX_A_DAK_135_0119 = '::pos{0,0,-4962101,-12043928,-2339889.2}',
EX_A_YJA_255_0121 = '::pos{0,0,5236583,-9051902,-857517.75}',
AD_A_GZH_654_0122 = '::pos{0,0,3267464.5,-5499737.5,-10219832}',
BA_A_VIU_490_0121 = '::pos{0,0,13996826,-8656656,-11248534}',
BA_A_TCT_520_0121 = '::pos{0,0,-6602390.5,-9652340,-14971225}',
AD_A_UVV_969_0122 = '::pos{0,0,-17383604,924578.9,1770260.8}',
UN_A_AHX_361_0123 = '::pos{0,0,-29066274,-13315.552,3336752.5}',
```
