# Tricorder.NET

## Overview
`Tricorder.NET` allows you to create tests that produce fully diagnostic reports.  Unlike most testing frameworks, `Tricroder.NET` does not exit a test on the first failed assertion.  Instead, it logs all the assertions made during the test, and then outputs the results.

## Setup

### Dependencies

`Tricorder.NET` uses the *Visual Studio Test Platform* to detect and run tests.  After installing `Tricorder.NET`, you may need to install `Microsoft.NET.Test.Sdk` before your IDE allows you run tests.

### Code

Once all dependencies have been added to the project, you can now create a test.  To access the functionality of `Tricorder.NET`, your class must extend `Test`.  Other than that, the syntax for test creation is identical to that of `MSTest`.

    [TestClass]  
    public class TransporterTets: Test  
    {  
        [TestMethod]  
        public void BeamMeUp()  
        {  
        }  
    }
## Assertions

### Methods
In order to validate your program state, you must use the assertion methods inherited from `Test`.   The methods available to you are listed in the table below.

| Name                   | Description                                            |
|------------------------|--------------------------------------------------------|
| AreEqual               | Are two values equal?                                  |
| AreNotEqual            | Are two values unequal?                                |
| IsTrue                 | Is the condition true?                                 |
| IsFalse                | Is the condition false?                                |
| IsNull                 | Is the value null?                                     |
| IsNotNull              | Is the value not null?                                 |
| IsGreaterThan          | Is the first value greater than second value?          |
| IsGreaterThanOrEqualTo | Is the first value greater than or equal second value? |
| IsLessThan             | Is the first value less than second value?             |
| IsLessThanOrEqualTo    | Is the first value less than or equal second value?    |
| Contains               | Does the set contain the element?                      |
| DoesNotContain         | Does the set exclude the element?                      |
| SequenceEqual          | Are the two sequences equal?                           |
| IsEmpty                | Does the set contain no elements?                      |
| IsNotEmpty             | Does the set contain at least one element?             |
| IsAssignableTo         | Is the first type assignable to the second type?       |
| IsInstanceOfType       | Can the value be assigned to the type?                 |
| TryGetValue            | Is the key present in the dictionary?                  |
| Throws                 | Does the action throw an exception?                    |

### Log Only Failures
If you only want to see invalid assertions, you can call `LogOnlyFailures` any time during the test execution.  Note, that all valid assertions will be removed from the log.  Also, if an exception is thrown before `LogOnlyFailures` is called, both valid and invalid assertions will be in the output.  It is best practice to call `LogOnlyFailures` before any assertions are made.

### Retry
You may come across a scenario where a test fails due to a timeout.  This is most common when testing web services, but it can happen with any asynchronous process.  To combat that, you can use the `Retry` method.  This allows you to run a test multiple times, either until there is a successful run or the number of attempts has been exhausted.  Only one of the following runs will be logged: the successful run or the final run. Be aware, you must have at least two attempts, otherwise you will get an exception.

    [TestMethod]  
    public void BeamMeUp()  
    {  
        Retry(GetTransporters, 3);  
    }  
      
    private static void GetTransporters()  
    {  
    }
