Flags
=====

A ``Flag`` represents some element of a set.

.. tabs::
   .. code-tab:: cs

      public class Flag<TSet>
      {
          public virtual bool IsAbstract { get; }

          public virtual TSet AddTo(TSet flags);

          public static TSet operator +(TSet value, Flag<TSet> flag);
 
          public virtual TSet RemoveFrom(TSet flags);

          public static TSet operator -(TSet value, Flag<TSet> flag);

          public virtual bool IsIn(TSet flags);
      }

   .. code-tab:: ts

      export class Flag<T> {
          public readonly isAbstract: boolean

          public addTo(flags: T): T

          public removeFrom(flags: T): T

          public isIn(flags: T): boolean
      }

Add to set
----------

Adds a flag and its parents to a set if they are not already present. This
operation will return a new set of flags, or the same set if it hasn't been
modified. The input set will never be modified in-place.

.. tabs::
   .. code-tab:: cs

      var newSet = flag.AddTo(mySet);
      var newSet = mySet + flag;
      mySet += flag;

   .. code-tab:: ts

      const newSet = flag.addTo(mySet);

.. versionchanged:: 2.0
   this method won't modify the set in-place anymore.
.. versionadded:: 1.0


Remove from set
---------------

Removes a flag and any of its children that appear in a set. This operation will
return a new set of flags, or the same set if it hasn't been modified. The input
set will never be modified in-place.

.. tabs::
   .. code-tab:: cs

      var newSet = flag.RemoveFrom(mySet);
      var newSet = mySet - flag;
      mySet -= flag;

   .. code-tab:: ts

      const newSet = flag.removeFrom(mySet);

.. versionchanged:: 2.0
   this method won't modify the set in-place anymore.
.. versionadded:: 1.0


Is in set
---------

Tests if a flag and all its parents are present in a set.

.. tabs::
   .. code-tab:: cs
   
      if(flag.IsIn(mySet)) {
          ...
      }

   .. code-tab:: ts

      if(flag.isIn(mySet)) {
          ...
      }

.. versionchanged:: 2.0
   this method is now called ``IsIn`` instead of ``In``.
.. versionadded:: 1.0


Is abstract
-----------

If this property is ``true``, it indicates that the flag has no value on its
own.

.. tabs::
   .. code-tab:: cs

      if(flag.IsAbstract) {
          ...
      }

   .. code-tab:: ts
   
      if(flag.isAbstract) {
          ...
      }

.. versionchanged:: 2.0
   a value of ``true`` is equivalent to either ``IsAbstract`` or ``IsNothing``
   being ``true`` in v1, and a value of ``false`` is equivalent to
   ``IsConcrete`` being ``true``.
.. versionadded:: 1.0