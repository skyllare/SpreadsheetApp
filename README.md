# Spreadsheet Application
This is a simple spreadsheet application that includes equation-solving capabilities. Has rows 1-50 and columns A-Z.

## Table of Contents


1. **[About this Project](#about-this-project)**<br>
2. **[Getting Started](#getting-started)**<br>
3. **[Functionality](#getting-started)**<br>
4. **[Authors](#authors)**<br>

## About This Project
### Built With
* C#
* .NET
* NUnit Testing
## Getting Started
1. Clone repository
```
git clone git@github.com:skyllare/SpreadsheetApp.git
```
2. add a project reference to SpreadSheet_Skyllar_Estill from SpreadsheetEngine.dll and ExpressionTree.dll<br>
   ![image](https://github.com/skyllare/SpreadsheetApp/assets/112673303/5252cc27-4484-490f-8d9c-7b1382030124)

## Functionality
### Equation Solving with Cell Values
The values of each cell can be set to a value. Another cell can reference these cells to do equations (+,-,*,/) on these values<br>
![math gif](https://github.com/skyllare/SpreadsheetApp/assets/112673303/33affeae-3df1-4b08-a689-25bdf4fe1385)

### Invalid Inputs
#### Circular Reference
Triggered when the last cell references the first cell, creating a closed loop<br>
https://github.com/skyllare/SpreadsheetApp/assets/112673303/c30d77a5-f0c4-4b08-a54d-656e893497dc
#### Bad Reference
Triggered when a cell that does not exist is referenced<br>

#### Self Reference
Triggered when a cell references itself. Works when cell references self in any capacity, including inside an equation<br>
![CircularReference](https://github.com/skyllare/SpreadsheetApp/assets/112673303/39a5fc8f-dc28-479e-8d3c-15154fba95af)

### Undo/ Redo

### Color Changing

### Save/ Load

## Authors
Skyllar Estill
skyllarestill@gmail.com




