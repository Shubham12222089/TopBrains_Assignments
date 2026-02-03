use Capgemini;
-- zipcode_info
alter table zipcode_info
alter column city varchar(25);


alter table zipcode_info
add state varchar(2);



-- instructor_info
alter table instructor_info
alter column instructor_first_name varchar(25);


alter table instructor_info
alter column instructor_last_name varchar(25);


alter table instructor_info
add street_address varchar(50),
    zip_code varchar(5);



-- course_info
alter table course_info
alter column cost numeric(9, 2);


alter table course_info
add course_name varchar(50),
    course_prerequisite numeric(8, 0);



-- student_info
alter table student_info
alter column student_first_name varchar(25);

alter table student_info
alter column student_last_name varchar(25);


alter table student_info
add street_address varchar(50),
    zip_code varchar(5);



-- section_info
alter table section_info
alter column section_no numeric(3, 0);

alter table section_info
add location varchar(50),
    capacity numeric(3, 0);



-- enrollment_info
alter table enrollment_info
add enrollment_date date;


-- grade_info
alter table grade_info
alter column grade_code_occurance numeric(38, 0);

alter table grade_info
add numeric_grade numeric(3, 0);

