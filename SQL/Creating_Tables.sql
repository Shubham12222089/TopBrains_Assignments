use Capgemini;
select Name from sys.tables;

create table zipcode_info (
    zip_code varchar(5),
    city varchar(10)
);


create table instructor_info (
    instructor_id numeric(8, 0),
    instructor_first_name varchar(15),
    instructor_last_name varchar(15)
);


create table course_info (
    course_no numeric(8, 0),
    cost numeric(5, 2)
);


create table student_info (
    student_id numeric(8, 0),
    student_first_name varchar(15),
    student_last_name varchar(15)
);


create table section_info (
    section_id numeric(8, 0),
    course_no numeric(8, 0),
    section_no numeric(5),
    instructor_id numeric(8, 0)
);


create table enrollment_info (
    student_id numeric(8, 0),
    section_id numeric(8, 0)
);


create table grade_info (
    student_id numeric(8, 0),
    section_id numeric(8, 0),
    grade_type_code char(2),
    grade_code_occurance numeric(5)
);

