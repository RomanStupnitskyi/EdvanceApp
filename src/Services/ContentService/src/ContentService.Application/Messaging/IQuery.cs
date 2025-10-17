using System.Diagnostics.CodeAnalysis;

namespace ContentService.Application.Messaging;

[SuppressMessage("Design", "CA1040:Avoid empty interfaces")]
[SuppressMessage("Major Code Smell", "S2326:Unused type parameters should be removed")]
public interface IQuery<TResponse>;