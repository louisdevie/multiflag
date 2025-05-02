Concepts
========

Flags hierarchy
---------------

.. mermaid::
   :align: center

   flowchart TD
      B(("B")) --> A(("A"))
      C(("C")) --> A
      D(("D")) --> B
      E(("E")) --> B
      F(("F")) --> C
      class E,B,A mmd-node-added