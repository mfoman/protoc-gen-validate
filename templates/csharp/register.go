package csharp

import (
	"bytes"
	"fmt"
	"os"
	"strings"
	"text/template"
	"unicode"

	"github.com/envoyproxy/protoc-gen-validate/templates/shared"
	"github.com/iancoleman/strcase"
	pgs "github.com/lyft/protoc-gen-star"
	pgsgo "github.com/lyft/protoc-gen-star/lang/go"
	"google.golang.org/protobuf/types/known/durationpb"
	"google.golang.org/protobuf/types/known/timestamppb"
)

func RegisterIndex(tpl *template.Template, params pgs.Parameters) {
	fns := csharpFuncs{pgsgo.InitContext(params)}

	tpl.Funcs(map[string]interface{}{
		"classNameFile": classNameFile,
		"importsPvg":    importsPvg,
		"csharpPackage":   csharpPackage,
		"simpleName":    fns.Name,
		"qualifiedName": fns.qualifiedName,
	})
}

func Register(tpl *template.Template, params pgs.Parameters) {
	fns := csharpFuncs{pgsgo.InitContext(params)}

	tpl.Funcs(map[string]interface{}{
		"accessor":                 fns.accessor,
		"byteArrayLit":             fns.byteArrayLit,
		"camelCase":                fns.camelCase,
		"classNameFile":            classNameFile,
		"classNameMessage":         classNameMessage,
		"durLit":                   fns.durLit,
		"fieldName":                fns.fieldName,
		"csharpPackage":              csharpPackage,
		"csharpStringEscape":         fns.csharpStringEscape,
		"csharpTypeFor":              fns.csharpTypeFor,
		"csharpTypeLiteralSuffixFor": fns.csharpTypeLiteralSuffixFor,
		"hasAccessor":              fns.hasAccessor,
		"oneof":                    fns.oneofTypeName,
		"sprintf":                  fmt.Sprintf,
		"simpleName":               fns.Name,
		"tsLit":                    fns.tsLit,
		"qualifiedName":            fns.qualifiedName,
		"isOfFileType":             fns.isOfFileType,
		"isOfMessageType":          fns.isOfMessageType,
		"isOfStringType":           fns.isOfStringType,
		"unwrap":                   fns.unwrap,
		"renderConstants":          fns.renderConstants(tpl),
		"constantName":             fns.constantName,
	})

	template.Must(tpl.Parse(fileTpl))
	template.Must(tpl.New("msg").Parse(msgTpl))
	template.Must(tpl.New("msgInner").Parse(msgInnerTpl))

	template.Must(tpl.New("none").Parse(noneTpl))

	template.Must(tpl.New("float").Parse(numTpl))
	template.Must(tpl.New("floatConst").Parse(numConstTpl))
	template.Must(tpl.New("double").Parse(numTpl))
	template.Must(tpl.New("doubleConst").Parse(numConstTpl))
	template.Must(tpl.New("int32").Parse(numTpl))
	template.Must(tpl.New("int32Const").Parse(numConstTpl))
	template.Must(tpl.New("int64").Parse(numTpl))
	template.Must(tpl.New("int64Const").Parse(numConstTpl))
	template.Must(tpl.New("uint32").Parse(numTpl))
	template.Must(tpl.New("uint32Const").Parse(numConstTpl))
	template.Must(tpl.New("uint64").Parse(numTpl))
	template.Must(tpl.New("uint64Const").Parse(numConstTpl))
	template.Must(tpl.New("sint32").Parse(numTpl))
	template.Must(tpl.New("sint32Const").Parse(numConstTpl))
	template.Must(tpl.New("sint64").Parse(numTpl))
	template.Must(tpl.New("sint64Const").Parse(numConstTpl))
	template.Must(tpl.New("fixed32").Parse(numTpl))
	template.Must(tpl.New("fixed32Const").Parse(numConstTpl))
	template.Must(tpl.New("fixed64").Parse(numTpl))
	template.Must(tpl.New("fixed64Const").Parse(numConstTpl))
	template.Must(tpl.New("sfixed32").Parse(numTpl))
	template.Must(tpl.New("sfixed32Const").Parse(numConstTpl))
	template.Must(tpl.New("sfixed64").Parse(numTpl))
	template.Must(tpl.New("sfixed64Const").Parse(numConstTpl))

	template.Must(tpl.New("bool").Parse(boolTpl))
	template.Must(tpl.New("string").Parse(stringTpl))
	template.Must(tpl.New("stringConst").Parse(stringConstTpl))
	template.Must(tpl.New("bytes").Parse(bytesTpl))
	template.Must(tpl.New("bytesConst").Parse(bytesConstTpl))

	template.Must(tpl.New("any").Parse(anyTpl))
	template.Must(tpl.New("anyConst").Parse(anyConstTpl))
	template.Must(tpl.New("enum").Parse(enumTpl))
	template.Must(tpl.New("enumConst").Parse(enumConstTpl))
	template.Must(tpl.New("message").Parse(messageTpl))
	template.Must(tpl.New("repeated").Parse(repeatedTpl))
	template.Must(tpl.New("repeatedConst").Parse(repeatedConstTpl))
	template.Must(tpl.New("map").Parse(mapTpl))
	template.Must(tpl.New("mapConst").Parse(mapConstTpl))
	template.Must(tpl.New("oneOf").Parse(oneOfTpl))
	template.Must(tpl.New("oneOfConst").Parse(oneOfConstTpl))

	template.Must(tpl.New("required").Parse(requiredTpl))
	template.Must(tpl.New("timestamp").Parse(timestampTpl))
	template.Must(tpl.New("timestampConst").Parse(timestampConstTpl))
	template.Must(tpl.New("duration").Parse(durationTpl))
	template.Must(tpl.New("durationConst").Parse(durationConstTpl))
	template.Must(tpl.New("wrapper").Parse(wrapperTpl))
	template.Must(tpl.New("wrapperConst").Parse(wrapperConstTpl))
}

type csharpFuncs struct{ pgsgo.Context }

func CsharpFilePath(f pgs.File, ctx pgsgo.Context, tpl *template.Template) *pgs.FilePath {
	// Don't generate validators for files that don't import PGV
	if !importsPvg(f) {
		return nil
	}

	fullPath := strings.Replace(csharpPackage(f), ".", string(os.PathSeparator), -1)
	fileName := classNameFile(f) + "Validator.cs"
	filePath := pgs.JoinPaths(fullPath, fileName)
	return &filePath
}

func CsharpMultiFilePath(f pgs.File, m pgs.Message) pgs.FilePath {
	fullPath := strings.Replace(csharpPackage(f), ".", string(os.PathSeparator), -1)
	fileName := classNameMessage(m) + "Validator.cs"
	filePath := pgs.JoinPaths(fullPath, fileName)
	return filePath
}

func importsPvg(f pgs.File) bool {
	for _, dep := range f.Descriptor().Dependency {
		if strings.HasSuffix(dep, "validate.proto") {
			return true
		}
	}
	return false
}

func classNameFile(f pgs.File) string {
	// Explicit outer class name overrides implicit name
	options := f.Descriptor().GetOptions()

	// if options != nil && !options.GetcsharpMultipleFiles() && options.csharpOuterClassname != nil {
	// 	return options.GetcsharpOuterClassname()
	// }
	if options != nil {
		return options.GetCsharpNamespace();
	}

	protoName := pgs.FilePath(f.Name().String()).BaseName()

	className := sanitizeClassName(protoName)
	className = appendOuterClassName(className, f)

	return className
}

func classNameMessage(m pgs.Message) string {
	className := m.Name().String()
	// This is really silly, but when the multiple files option is true, protoc puts underscores in file names.
	// When multiple files is false, underscores are stripped. Short of rewriting all the name sanitization
	// logic for csharp, using "UnderscoreUnderscoreUnderscore" is an escape sequence seems to work with an extremely
	// small likelihood of name conflict.
	className = strings.Replace(className, "_", "UnderscoreUnderscoreUnderscore", -1)
	className = sanitizeClassName(className)
	className = strings.Replace(className, "UnderscoreUnderscoreUnderscore", "_", -1)
	return className
}

func sanitizeClassName(className string) string {
	className = makeInvalidClassnameCharactersUnderscores(className)
	className = underscoreBetweenConsecutiveUppercase(className)
	className = strcase.ToCamel(strcase.ToSnake(className))
	className = upperCaseAfterNumber(className)
	return className
}

func csharpPackage(file pgs.File) string {
	// Explicit csharp package overrides implicit package
	options := file.Descriptor().GetOptions()
	if options != nil && options.CsharpNamespace != nil {
		return options.GetCsharpNamespace()
	}
	return file.Package().ProtoName().String()
}

func (fns csharpFuncs) qualifiedName(entity pgs.Entity) string {
	file, isFile := entity.(pgs.File)
	if isFile {
		name := csharpPackage(file)
		if file.Descriptor().GetOptions() != nil {
			if file.Descriptor().GetOptions().CsharpNamespace == nil {
				name += ("." + classNameFile(file))
			}
		} else {
			name += ("." + classNameFile(file))
		}
		return name
	}

	message, isMessage := entity.(pgs.Message)
	if isMessage && message.Parent() != nil {
		// recurse
		return fns.qualifiedName(message.Parent()) + "." + entity.Name().String()
	}

	enum, isEnum := entity.(pgs.Enum)
	if isEnum && enum.Parent() != nil {
		// recurse
		return fns.qualifiedName(enum.Parent()) + "." + entity.Name().String()
	}

	return entity.Name().String()
}

// Replace invalid identifier characters with an underscore
func makeInvalidClassnameCharactersUnderscores(name string) string {
	var sb string
	for _, c := range name {
		switch {
		case c >= '0' && c <= '9':
			sb += string(c)
		case c >= 'a' && c <= 'z':
			sb += string(c)
		case c >= 'A' && c <= 'Z':
			sb += string(c)
		default:
			sb += "_"
		}
	}
	return sb
}

func upperCaseAfterNumber(name string) string {
	var sb string
	var p rune

	for _, c := range name {
		if unicode.IsDigit(p) {
			sb += string(unicode.ToUpper(c))
		} else {
			sb += string(c)
		}
		p = c
	}
	return sb
}

func underscoreBetweenConsecutiveUppercase(name string) string {
	var sb string
	var p rune

	for _, c := range name {
		if unicode.IsUpper(p) && unicode.IsUpper(c) {
			sb += "_" + string(c)
		} else {
			sb += string(c)
		}
		p = c
	}
	return sb
}

func appendOuterClassName(outerClassName string, file pgs.File) string {
	conflict := false

	for _, enum := range file.AllEnums() {
		if enum.Name().String() == outerClassName {
			conflict = true
		}
	}

	for _, message := range file.AllMessages() {
		if message.Name().String() == outerClassName {
			conflict = true
		}
	}

	for _, service := range file.Services() {
		if service.Name().String() == outerClassName {
			conflict = true
		}
	}

	if conflict {
		return outerClassName + "OuterClass"
	} else {
		return outerClassName
	}
}

func (fns csharpFuncs) accessor(ctx shared.RuleContext) string {
	if ctx.AccessorOverride != "" {
		return ctx.AccessorOverride
	}
	return fns.fieldAccessor(ctx.Field)
}

func (fns csharpFuncs) fieldAccessor(f pgs.Field) string {
	fieldName := strcase.ToCamel(f.Name().String())
	if f.Type().IsMap() {
		fieldName += "Map"
	}
	if f.Type().IsRepeated() {
		fieldName += "List"
	}

	fieldName = upperCaseAfterNumber(fieldName)
	return fmt.Sprintf("proto.get%s()", fieldName)
}

func (fns csharpFuncs) hasAccessor(ctx shared.RuleContext) string {
	if ctx.AccessorOverride != "" {
		return "true"
	}
	fiedlName := strcase.ToCamel(ctx.Field.Name().String())
	fiedlName = upperCaseAfterNumber(fiedlName)
	return "proto.has" + fiedlName + "()"
}

func (fns csharpFuncs) fieldName(ctx shared.RuleContext) string {
	return ctx.Field.Name().String()
}

func (fns csharpFuncs) csharpTypeFor(ctx shared.RuleContext) string {
	t := ctx.Field.Type()

	// Map key and value types
	if t.IsMap() {
		switch ctx.AccessorOverride {
		case "key":
			return fns.csharpTypeForProtoType(t.Key().ProtoType())
		case "value":
			return fns.csharpTypeForProtoType(t.Element().ProtoType())
		}
	}

	if t.IsEmbed() {
		if embed := t.Embed(); embed.IsWellKnown() {
			switch embed.WellKnownType() {
			case pgs.AnyWKT:
				return "String"
			case pgs.DurationWKT:
				return "com.google.protobuf.Duration"
			case pgs.TimestampWKT:
				return "com.google.protobuf.Timestamp"
			case pgs.Int32ValueWKT, pgs.UInt32ValueWKT:
				return "Integer"
			case pgs.Int64ValueWKT, pgs.UInt64ValueWKT:
				return "Long"
			case pgs.DoubleValueWKT:
				return "Double"
			case pgs.FloatValueWKT:
				return "Float"
			}
		}
	}

	if t.IsRepeated() {
		if t.ProtoType() == pgs.MessageT {
			return fns.qualifiedName(t.Element().Embed())
		} else if t.ProtoType() == pgs.EnumT {
			return fns.qualifiedName(t.Element().Enum())
		}
	}

	if t.IsEnum() {
		return fns.qualifiedName(t.Enum())
	}

	return fns.csharpTypeForProtoType(t.ProtoType())
}

func (fns csharpFuncs) csharpTypeForProtoType(t pgs.ProtoType) string {

	switch t {
	case pgs.Int32T, pgs.UInt32T, pgs.SInt32, pgs.Fixed32T, pgs.SFixed32:
		return "Integer"
	case pgs.Int64T, pgs.UInt64T, pgs.SInt64, pgs.Fixed64T, pgs.SFixed64:
		return "Long"
	case pgs.DoubleT:
		return "Double"
	case pgs.FloatT:
		return "Float"
	case pgs.BoolT:
		return "Boolean"
	case pgs.StringT:
		return "String"
	case pgs.BytesT:
		return "com.google.protobuf.ByteString"
	default:
		return "Object"
	}
}

func (fns csharpFuncs) csharpTypeLiteralSuffixFor(ctx shared.RuleContext) string {
	t := ctx.Field.Type()

	if t.IsMap() {
		switch ctx.AccessorOverride {
		case "key":
			return fns.csharpTypeLiteralSuffixForPrototype(t.Key().ProtoType())
		case "value":
			return fns.csharpTypeLiteralSuffixForPrototype(t.Element().ProtoType())
		}
	}

	if t.IsEmbed() {
		if embed := t.Embed(); embed.IsWellKnown() {
			switch embed.WellKnownType() {
			case pgs.Int64ValueWKT, pgs.UInt64ValueWKT:
				return "L"
			case pgs.FloatValueWKT:
				return "F"
			case pgs.DoubleValueWKT:
				return "D"
			}
		}
	}

	return fns.csharpTypeLiteralSuffixForPrototype(t.ProtoType())
}

func (fns csharpFuncs) csharpTypeLiteralSuffixForPrototype(t pgs.ProtoType) string {
	switch t {
	case pgs.Int64T, pgs.UInt64T, pgs.SInt64, pgs.Fixed64T, pgs.SFixed64:
		return "L"
	case pgs.FloatT:
		return "F"
	case pgs.DoubleT:
		return "D"
	default:
		return ""
	}
}

func (fns csharpFuncs) csharpStringEscape(s string) string {
	s = fmt.Sprintf("%q", s)
	s = s[1 : len(s)-1]
	s = strings.Replace(s, `\u00`, `\x`, -1)
	s = strings.Replace(s, `\x`, `\\x`, -1)
	// s = strings.Replace(s, `\`, `\\`, -1)
	s = strings.Replace(s, `"`, `\"`, -1)
	return `"` + s + `"`
}

func (fns csharpFuncs) camelCase(name pgs.Name) string {
	return strcase.ToCamel(name.String())
}

func (fns csharpFuncs) byteArrayLit(bytes []uint8) string {
	var sb string
	sb += "new byte[]{"
	for _, b := range bytes {
		sb += fmt.Sprintf("(byte)%#x,", b)
	}
	sb += "}"

	return sb
}

func (fns csharpFuncs) durLit(dur *durationpb.Duration) string {
	return fmt.Sprintf(
		"io.envoyproxy.pgv.TimestampValidation.toDuration(%d,%d)",
		dur.GetSeconds(), dur.GetNanos())
}

func (fns csharpFuncs) tsLit(ts *timestamppb.Timestamp) string {
	return fmt.Sprintf(
		"io.envoyproxy.pgv.TimestampValidation.toTimestamp(%d,%d)",
		ts.GetSeconds(), ts.GetNanos())
}

func (fns csharpFuncs) oneofTypeName(f pgs.Field) pgsgo.TypeName {
	return pgsgo.TypeName(strings.ToUpper(f.Name().String()))
}

func (fns csharpFuncs) isOfFileType(o interface{}) bool {
	switch o.(type) {
	case pgs.File:
		return true
	default:
		return false
	}
}

func (fns csharpFuncs) isOfMessageType(f pgs.Field) bool {
	return f.Type().ProtoType() == pgs.MessageT
}

func (fns csharpFuncs) isOfStringType(f pgs.Field) bool {
	return f.Type().ProtoType() == pgs.StringT
}

func (fns csharpFuncs) unwrap(ctx shared.RuleContext) (shared.RuleContext, error) {
	ctx, err := ctx.Unwrap("wrapped")
	if err != nil {
		return ctx, err
	}
	ctx.AccessorOverride = fmt.Sprintf("%s.get%s()", fns.fieldAccessor(ctx.Field),
		fns.camelCase(ctx.Field.Type().Embed().Fields()[0].Name()))
	return ctx, nil
}

func (fns csharpFuncs) renderConstants(tpl *template.Template) func(ctx shared.RuleContext) (string, error) {
	return func(ctx shared.RuleContext) (string, error) {
		var b bytes.Buffer
		var err error

		hasConstTemplate := false
		for _, t := range tpl.Templates() {
			if t.Name() == ctx.Typ+"Const" {
				hasConstTemplate = true
			}
		}

		if hasConstTemplate {
			err = tpl.ExecuteTemplate(&b, ctx.Typ+"Const", ctx)
		}

		return b.String(), err
	}
}

func (fns csharpFuncs) constantName(ctx shared.RuleContext, rule string) string {
	return strcase.ToScreamingSnake(ctx.Field.Name().String() + "_" + ctx.Index + "_" + rule)
}
