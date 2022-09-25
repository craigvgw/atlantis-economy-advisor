namespace advisor {
    public interface IParser {
        Maybe<IReportNode> Parse(TextParser p);
    }

    public abstract class BaseParser : IParser {
        public Maybe<IReportNode> Parse(TextParser p) => p.Try(Execute);

        protected abstract Maybe<IReportNode> Execute(TextParser p);

        protected Maybe<IReportNode> Ok(IReportNode result) => new Maybe<IReportNode>(result);

        protected Maybe<IReportNode> Error<T>(Maybe<T> p) => p.Convert<IReportNode>();

        protected Maybe<TextParser> LastResult;

        protected Maybe<TextParser> Mem(Maybe<TextParser> result) {
            LastResult = result;
            return result;
        }
    }

    public static class ReportParserExtensions {
        public static Maybe<IReportNode> ParseMaybe(this IParser parser, Maybe<TextParser> p)
            => p ? parser.Parse(p.Value) : p.Convert<IReportNode>();
    }
}
