﻿// ****************************************************************
// Copyright (c) 2011 NUnit Software. All rights reserved.
// ****************************************************************

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Core;
using TestResult = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult;

namespace NUnit.VisualStudio.TestAdapter
{
    [Serializable]
    public class NUnitEventListener : EventListener // Public for testing
    {
        private ITestExecutionRecorder testLog;
        private Dictionary<string, TestCase> map;
        private string assemblyName;
        private TestConverter testConverter;

        public NUnitEventListener(ITestExecutionRecorder testLog, Dictionary<string, TestCase> map, string assemblyName)
        {
            this.testLog = testLog;
            this.map = map;
            this.assemblyName = assemblyName;
            this.testConverter = new TestConverter(map);
        }

        public void RunStarted(string name, int testCount)
        {
        }

        public void RunFinished(Exception exception)
        {
        }

        public void RunFinished(NUnit.Core.TestResult result)
        {
        }

        public void SuiteStarted(TestName testName)
        {
        }

        public void SuiteFinished(NUnit.Core.TestResult result)
        {
        }

        public void TestStarted(TestName testName)
        {
            TestCase ourCase = testConverter.ConvertTestName(testName, assemblyName);
            ourCase.Source = assemblyName;
            this.testLog.RecordStart(ourCase);
        }

        public void TestFinished(NUnit.Core.TestResult result)
        {
            TestResult ourResult = testConverter.ConvertTestResult(result);
            ourResult.TestCase.Source = this.assemblyName;
            this.testLog.RecordResult(ourResult);
        }

        public void TestOutput(TestOutput testOutput)
        {
        }

        public void UnhandledException(Exception exception)
        {
        }
    }
}
