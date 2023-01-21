package csharp

const anyConstTpl = `{{ $f := .Field }}{{ $r := .Rules }}
{{- if $r.In }}
		private readonly String[] {{ constantName . "In" }} = new String[] {
			{{- range $r.In }}
			"{{ . }}",
			{{- end }}
		};
{{- end -}}
{{- if $r.NotIn }}
		private readonly String[] {{ constantName . "NotIn" }} = new String[] {
			{{- range $r.NotIn }}
			"{{ . }}",
			{{- end }}
		};
{{- end -}}`

const anyTpl = `{{ $f := .Field }}{{ $r := .Rules }}
	{{- template "required" . -}}

	{{- if $r.In }}
			if ({{ hasAccessor . }}) io.envoyproxy.pgv.CollectiveValidation.in("{{ $f.FullyQualifiedName }}", {{ accessor . }}.getTypeUrl(), {{ constantName . "In" }});
	{{- end -}}
	{{- if $r.NotIn }}
			if ({{ hasAccessor . }}) io.envoyproxy.pgv.CollectiveValidation.notIn("{{ $f.FullyQualifiedName }}", {{ accessor . }}.getTypeUrl(), {{ constantName . "NotIn" }});
	{{- end -}}
`
