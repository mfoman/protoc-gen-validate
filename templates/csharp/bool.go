package csharp

const boolTpl = `{{ $f := .Field }}{{ $r := .Rules -}}
{{- if $r.Const }}
			io.envoyproxy.pgv.ConstantValidation.Constant("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ $r.GetConst }});
{{- end }}`
