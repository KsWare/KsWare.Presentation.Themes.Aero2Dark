using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero2DarkTestApp;
public static class ObjectExtensions {

	private static ConcurrentDictionary<object, long> _objectIds = new ConcurrentDictionary<object, long>();
	private static long _nextId = 0;

	public static long GetObjectId(this object obj) {
		return _objectIds.GetOrAdd(obj, _ => System.Threading.Interlocked.Increment(ref _nextId));
	}
}
