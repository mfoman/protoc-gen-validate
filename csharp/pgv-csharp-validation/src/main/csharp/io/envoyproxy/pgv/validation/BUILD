load("@rules_dotnet//dotnet:defs.bzl", "csharp_library")

csharp_library(
    name = "csharp_harness",
    srcs = glob(["*.cs"]),
    visibility = ["//tests/harness:__subpackages__"],
    deps = [
        "//csharp/pgv-csharp-stub/src/main/csharp/io/envoyproxy/pgv",
        "//csharp/pgv-csharp-validation/src/main/csharp/io/envoyproxy/pgv",
        "//tests/harness:harness_csharp_proto",
        "//tests/harness/cases:cases_csharp_proto",
        "//tests/harness/cases:csharp",
        "//tests/harness/cases/other_package:embed_csharp_proto",
        "//tests/harness/cases/other_package:csharp",
        "//tests/harness/cases/yet_another_package:embed_csharp_proto",
        "//tests/harness/cases/yet_another_package:csharp",
        "//validate:validate_csharp",
        "@com_google_guava//jar",
        "@com_google_protobuf//:protobuf_csharp",
    ],
)
