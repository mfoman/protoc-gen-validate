package csharp

const enumConstTpl = `{{ $ctx := . }}{{ $f := .Field }}{{ $r := .Rules -}}
{{- if $r.In }}
		private readonly {{ csharpTypeFor . }}[] {{ constantName . "In" }} = new {{ csharpTypeFor . }}[]{
			{{- range $r.In }}
			{{ csharpTypeFor $ctx }}.forNumber({{- sprintf "%v" . -}}),
			{{- end }}
		};
{{- end -}}
{{- if $r.NotIn }}
		private readonly {{ csharpTypeFor . }}[] {{ constantName . "NotIn" }} = new {{ csharpTypeFor . }}[]{
			{{- range $r.NotIn }}
			{{ csharpTypeFor $ctx }}.forNumber({{- sprintf "%v" . -}}),
			{{- end }}
		};
{{- end -}}`

const enumTpl = `{{ $f := .Field }}{{ $r := .Rules -}}
{{- if $r.Const }}
			io.envoyproxy.pgv.ConstantValidation.constant("{{ $f.FullyQualifiedName }}", {{ accessor . }}, 
				{{ csharpTypeFor . }}.forNumber({{ $r.GetConst }}));
{{- end -}}
{{- if $r.GetDefinedOnly }}
			io.envoyproxy.pgv.EnumValidation.definedOnly("{{ $f.FullyQualifiedName }}", {{ accessor . }});
{{- end -}}
{{- if $r.In }}
			io.envoyproxy.pgv.CollectiveValidation.in("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "In" }});
{{- end -}}
{{- if $r.NotIn }}
			io.envoyproxy.pgv.CollectiveValidation.notIn("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "NotIn" }});
{{- end -}}
`
