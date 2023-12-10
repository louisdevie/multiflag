import bs4
import sys
import time
import copy


def main(argv: list[str]):
    sources: list[CoverageSource] = []

    for report_path in argv[1:]:
        with open(report_path, "rt", encoding="utf-8") as report_file:
            sources.append(CoverageSource(report_file.read()))

    branches_total = CoverageStats(0, 0)
    lines_total = CoverageStats(0, 0)

    for source in sources:
        branches_total += source.branch_stats
        lines_total += source.line_stats

    soup = bs4.BeautifulSoup(features="xml")

    soup.append(
        bs4.Doctype(
            'coverage SYSTEM "http://cobertura.sourceforge.net/xml/coverage-04.dtd"'
        )
    )

    coverage = soup.new_tag(
        "coverage",
        attrs={
            "branch-rate": branches_total.rate,
            "branches-covered": branches_total.covered,
            "branches-valid": branches_total.valid,
            "line-rate": lines_total.rate,
            "lines-covered": lines_total.covered,
            "lines-valid": lines_total.valid,
            "timestamp": int(time.time()),
        },
    )

    sources_tag = soup.new_tag("sources")
    packages_tag = soup.new_tag("packages")

    for source in sources:
        source_tag = soup.new_tag("source")
        source_tag.append(source.source)
        sources_tag.append(source_tag)

        for package in source.packages.children:
            packages_tag.append(copy.copy(package))

    coverage.append(sources_tag)
    coverage.append(packages_tag)

    soup.append(coverage)

    print(soup)


class CoverageStats:
    __covered: int
    __valid: int

    def __init__(self, covered: int, valid: int):
        self.__covered = covered
        self.__valid = valid

    def __add__(self, other):
        if isinstance(other, CoverageStats):
            return CoverageStats(
                self.__covered + other.__covered,
                self.__valid + other.__valid,
            )
        else:
            return NotImplemented

    @property
    def covered(self) -> int:
        return self.__covered

    @property
    def valid(self) -> int:
        return self.__valid

    @property
    def rate(self) -> float:
        return self.__covered / self.__valid


class CoverageSource:
    __packages: bs4.Tag
    __source: str
    __branch_stats: CoverageStats
    __line_stats: CoverageStats

    def __init__(self, markup: str):
        soup = bs4.BeautifulSoup(markup, "xml")
        coverage = soup.coverage

        assert coverage is not None

        self.__branch_stats = CoverageStats(
            CoverageSource.__get_int_attribute(coverage, "branches-covered"),
            CoverageSource.__get_int_attribute(coverage, "branches-valid"),
        )
        self.__line_stats = CoverageStats(
            CoverageSource.__get_int_attribute(coverage, "lines-covered"),
            CoverageSource.__get_int_attribute(coverage, "lines-valid"),
        )

        assert coverage.source is not None
        self.__source = coverage.source.getText()

        assert coverage.packages is not None
        self.__packages = coverage.packages

    @staticmethod
    def __get_int_attribute(tag: bs4.Tag, attr: str) -> int:
        values = tag[attr]
        return int(values if isinstance(values, str) else values[0])

    @property
    def branch_stats(self) -> CoverageStats:
        return self.__branch_stats

    @property
    def line_stats(self) -> CoverageStats:
        return self.__line_stats

    @property
    def source(self) -> str:
        return self.__source

    @property
    def packages(self) -> bs4.Tag:
        return self.__packages


if __name__ == "__main__":
    main(sys.argv)
