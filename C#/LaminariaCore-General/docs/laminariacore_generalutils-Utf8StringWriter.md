# Utf8StringWriter `Internal class`

## Description
This class extends the functionality of the StringWriter class, defining its
            encoding in utf-8.

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph LaminariaCore_General.utils
  LaminariaCore_General.utils.Utf8StringWriter[[Utf8StringWriter]]
  end
  subgraph System.IO
System.IO.StringWriter[[StringWriter]]
  end
System.IO.StringWriter --> LaminariaCore_General.utils.Utf8StringWriter
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `Encoding` | [`Encoding`](#encoding) | `get` |

## Details
### Summary
This class extends the functionality of the StringWriter class, defining its
            encoding in utf-8.

### Inheritance
 - `StringWriter`

### Constructors
#### Utf8StringWriter
```csharp
public Utf8StringWriter()
```

### Properties
#### Encoding
```csharp
public override Encoding Encoding { get; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
