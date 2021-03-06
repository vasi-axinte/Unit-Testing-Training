﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Samples._11_StubVsMock
{
	[TestClass]
	public class PersonLoggerTests
	{
		[TestMethod]
		public void Log_PersonAndMessageAreGiven_LogLineFormated()
		{
			FileWriterDouble fileWriterMock = new FileWriterDouble();
			FileWriterFactoryDouble fileWriterFactoryStub = new FileWriterFactoryDouble(fileWriterMock);

			var target = new PersonLogger(fileWriterFactoryStub);

			target.Log(new Person {Name = "Florin Coros"}, "CREATED");

			const string expected = "Person: Florin Coros | Message: CREATED";
			Assert.AreEqual(expected, fileWriterMock.Line);
		}


		private class FileWriterDouble : IFileWriter
		{
			public void WriteLine(string line)
			{
				Line = line;
			}

			public string Line { get; private set; }
		}

		private class FileWriterFactoryDouble : IFileWriterFactory
		{
			private readonly IFileWriter fileWriter;

			public FileWriterFactoryDouble(IFileWriter fileWriter)
			{
				this.fileWriter = fileWriter;
			}

			public IFileWriter GetNewWriter(string fileName)
			{
				return fileWriter;
			}
		}
	}
}