using System.Diagnostics.CodeAnalysis;

namespace ContentService.SharedKernel;

[SuppressMessage("Design", "CA1002:Do not expose generic lists")]
[SuppressMessage("Design", "CA1030:Use events where appropriate")]
public abstract class Entity
{
	private readonly List<IDomainEvent> _domainEvents = [];

	public List<IDomainEvent> DomainEvents => [.. _domainEvents];

	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}

	public void Raise(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
}
