using System;
using System.Collections;
using System.Reactive.Subjects;
using System.Text;

public class TestBase
{
    class Output : ITestOutputHelper
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }
    }

    readonly ITestOutputHelper output;
    protected readonly StringBuilder testOutput = new StringBuilder();
    protected DateTimeOffset Now = new DateTimeOffset(0, TimeSpan.FromTicks(0));

    public TestBase()
    {
        output = new Output();
    }

    public TestBase(ITestOutputHelper output)
    {
        this.output = output;
    }


    protected void Dump(string msg)
    {
        output?.WriteLine(msg);
        testOutput.AppendLine(msg);
    }

    protected void Dump(object prefix, object thing)
    {
        output?.WriteLine($"{prefix} - {thing}");
        testOutput.AppendLine($"{prefix} - {thing}");
    }

    protected void Dump(object thing)
    {
        output?.WriteLine(thing.ToString());
        testOutput.AppendLine(thing.ToString());
    }
    protected void Dump(string title, IEnumerable col)
    {
        output?.WriteLine(title);
        testOutput.AppendLine(title);
        var i = 0;
        foreach (var l in col)
            Dump(i++, l);
    }

    protected void Dump(IEnumerable col)
    {
        Dump("Dumping", col);
    }

    protected bool Compare(Thing thing1, Thing thing2)
    {
        return Equals(thing1, thing2) && thing1.Title == thing2.Title && thing1.CreatedAt == thing2.CreatedAt && thing1.UpdatedAt == thing2.UpdatedAt;
    }

    protected void Add(Subject<Thing> source, Thing item)
    {
        source.OnNext(item);
    }

    protected Thing GetThing(int id)
    {
        return new Thing { Number = id };
    }

    protected Thing GetThing(int id, int minutes)
    {
        return new Thing { Number = id, Title = "Run 1", CreatedAt = Now + TimeSpan.FromMinutes(minutes), UpdatedAt = Now + TimeSpan.FromMinutes(minutes) };
    }

    protected Thing GetThing(int id, int minutesc, int minutesu)
    {
        return new Thing { Number = id, Title = "Run 1", CreatedAt = Now + TimeSpan.FromMinutes(minutesc), UpdatedAt = Now + TimeSpan.FromMinutes(minutesu) };
    }

    protected Thing GetThing(int id, string title)
    {
        return new Thing { Number = id, Title = "Run 1" };
    }

    protected Thing GetThing(int id, int minutesc, int minutesu, string title)
    {
        return new Thing { Number = id, Title = title, CreatedAt = Now + TimeSpan.FromMinutes(minutesc), UpdatedAt = Now + TimeSpan.FromMinutes(minutesu) };
    }
}