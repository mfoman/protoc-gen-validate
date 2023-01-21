package csharp

const numConstTpl = `{{ $f := .Field }}{{ $r := .Rules -}}
{{- if $r.Const }}
		private readonly {{ csharpTypeFor .}} {{ constantName . "Const" }} = {{ $r.GetConst }}{{ csharpTypeLiteralSuffixFor . }};
{{- end -}}
{{- if $r.Lt }}
		private readonly {{ csharpTypeFor .}} {{ constantName . "Lt" }} = {{ $r.GetLt }}{{ csharpTypeLiteralSuffixFor . }};
{{- end -}}
{{- if $r.Lte }}
		private readonly {{ csharpTypeFor .}} {{ constantName . "Lte" }} = {{ $r.GetLte }}{{ csharpTypeLiteralSuffixFor . }};
{{- end -}}
{{- if $r.Gt }}
		private readonly {{ csharpTypeFor .}} {{ constantName . "Gt" }} = {{ $r.GetGt }}{{ csharpTypeLiteralSuffixFor . }};
{{- end -}}
{{- if $r.Gte }}
		private readonly {{ csharpTypeFor .}} {{ constantName . "Gte" }} = {{ $r.GetGte }}{{ csharpTypeLiteralSuffixFor . }};
{{- end -}}
{{- if $r.In }}
		private readonly {{ csharpTypeFor . }}[] {{ constantName . "In" }} = new {{ csharpTypeFor . }}[]{
			{{- range $r.In -}}
				{{- sprintf "%v" . -}}{{ csharpTypeLiteralSuffixFor $ }},
			{{- end -}}
		};
{{- end -}}
{{- if $r.NotIn }}
		private readonly {{ csharpTypeFor . }}[] {{ constantName . "NotIn" }} = new {{ csharpTypeFor . }}[]{
			{{- range $r.NotIn -}}
				{{- sprintf "%v" . -}}{{ csharpTypeLiteralSuffixFor $ }},
			{{- end -}}
		};
{{- end -}}`

const numTpl = `{{ $f := .Field }}{{ $r := .Rules -}}
{{- if $r.GetIgnoreEmpty }}
			if ( {{ accessor . }} != 0 ) {
{{- end -}}
{{- if $r.Const }}
			io.envoyproxy.pgv.ConstantValidation.constant("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "Const" }});
{{- end -}}
{{- if and (or $r.Lt $r.Lte) (or $r.Gt $r.Gte)}}
			io.envoyproxy.pgv.ComparativeValidation.range("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ if $r.Lt }}{{ constantName . "Lt" }}{{ else }}null{{ end }}, {{ if $r.Lte }}{{ constantName . "Lte" }}{{ else }}null{{ end }}, {{ if $r.Gt }}{{ constantName . "Gt" }}{{ else }}null{{ end }}, {{ if $r.Gte }}{{ constantName . "Gte" }}{{ else }}null{{ end }}, csharp.util.Comparator.naturalOrder());
{{- else -}}
{{- if $r.Lt }}
			io.envoyproxy.pgv.ComparativeValidation.lessThan("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "Lt" }}, csharp.util.Comparator.naturalOrder());
{{- end -}}
{{- if $r.Lte }}
			io.envoyproxy.pgv.ComparativeValidation.lessThanOrEqual("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "Lte" }}, csharp.util.Comparator.naturalOrder());
{{- end -}}
{{- if $r.Gt }}
			io.envoyproxy.pgv.ComparativeValidation.greaterThan("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "Gt" }}, csharp.util.Comparator.naturalOrder());
{{- end -}}
{{- if $r.Gte }}
			io.envoyproxy.pgv.ComparativeValidation.greaterThanOrEqual("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "Gte" }}, csharp.util.Comparator.naturalOrder());
{{- end -}}
{{- end -}}
{{- if $r.In }}
			io.envoyproxy.pgv.CollectiveValidation.in("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "In" }});
{{- end -}}
{{- if $r.NotIn }}
			io.envoyproxy.pgv.CollectiveValidation.notIn("{{ $f.FullyQualifiedName }}", {{ accessor . }}, {{ constantName . "NotIn" }});
{{- end -}}
{{- if $r.GetIgnoreEmpty }}
			}
{{- end -}}
`
