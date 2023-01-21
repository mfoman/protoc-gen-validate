load("@rules_dotnet//dotnet:defs.bzl", "csharp_library")

csharp_library(
    name = "pgv",
    srcs = glob(["*.cs"]),
    visibility = ["//visibility:public"],
    deps = [
        "//external:guava",
        "@com_google_protobuf//:protobuf_csharp",
        "@com_google_protobuf//:protobuf_csharp_util",
        # "@com_google_re2j//jar",
        # "@org_apache_commons_validator//jar",
    ],
)
