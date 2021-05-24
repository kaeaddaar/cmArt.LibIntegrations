Purpose: InfoInterfaces (II_) is a place to put the logical groupings for interfaces so they can be shared more easily 
between various integration code. Sort of like a "Partial" Interface
Ex: Create an IPrices for storing the Price Level, Formula, Percentage, Price, and possibly the Quantity Break Point
Ex: Create a ISupplierInfo for storing the Supplier, Brand, Supplier Part Number, and possible alternate supplier info

As we flesh out this structure it will make it easier to pass the chunks around. We should actually even use these 
"partial" interfaces when performing any logic or transformation procedures making it easier to share that logic

To add clarity that we are using these Information Interfaces we could use II_ as a prefix to clearly denote them

