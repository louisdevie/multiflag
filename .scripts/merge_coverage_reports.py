import json
import sys


def main(argv: list[str]):
    reports = []

    for report_path in argv[1:]:
        with open(report_path, "rt", encoding="utf-8") as report_file:
            reports.append(json.load(report_file))

    for report in reports:
        print(report)


if __name__ == "__main__":
    main(sys.argv)
