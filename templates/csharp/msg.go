package csharp

const msgTpl = `
{{ if not (ignored .) -}}
	/**
	 * Validates {@code {{ simpleName . }}} protobuf objects.
	 */
	public static class {{ simpleName . }}Validator : io.envoyproxy.pgv.ValidatorImpl<{{ qualifiedName . }}> {
		{{- template "msgInner" . -}}
	}
{{- end -}}
`

// throws io.envoyproxy.pgv.ValidationException 

const msgInnerTpl = `
	{{- range .NonOneOfFields }}
		{{ renderConstants (context .) }}
	{{ end }}
	{{ range .SyntheticOneOfFields }}
		{{ renderConstants (context .) }}
	{{ end }}
	{{ range .RealOneOfs }}
		{{ template "oneOfConst" . }}
	{{ end }}

	public void assertValid({{ qualifiedName . }} proto, io.envoyproxy.pgv.ValidatorIndex index) {
	{{ if disabled . }}
		// Validate is disabled for {{ simpleName . }}
		return;
	{{- else -}}
	{{ range .NonOneOfFields -}}
		{{ render (context .) }}
	{{ end -}}
	{{ range .SyntheticOneOfFields }}
		if ({{ hasAccessor (context .) }}) {
			{{ render (context .) }}
		}
	{{ end }}
	{{ range .RealOneOfs }}
		{{ template "oneOf" . }}
	{{- end -}}
	{{- end }}
	}
`
