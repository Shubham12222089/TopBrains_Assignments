alter table zipcode_info
alter column zip_code varchar(5) not null;

alter table zipcode_info
alter column city varchar(25) not null;

alter table zipcode_info
alter column state varchar(2) not null;

alter table zipcode_info
add constraint zip_pk
primary key (zip_code);


-------------instructor_info----------------------
alter table instructor_info
alter column instructor_id numeric(8,0) not null;

alter table instructor_info
alter column instructor_first_name varchar(25) not null;

alter table instructor_info
alter column instructor_last_name varchar(25) not null;

alter table instructor_info
add constraint instructor_id_pk
primary key (instructor_id);

alter table instructor_info
add constraint zip_instructor_fk
foreign key (zip_code)
references zipcode_info (zip_code);

---------------course_info-----------------------
alter table course_info
alter column course_no numeric(8,0) not null;

alter table course_info
alter column course_name varchar(50) not null;

alter table course_info
alter column cost numeric(9,2) not null;

alter table course_info
add constraint course_no_pk
primary key (course_no);
----------------student_info----------------------
alter table student_info
alter column student_id numeric(8,0) not null;

alter table student_info
alter column student_first_name varchar(25) not null;

alter table student_info
alter column student_last_name varchar(25) not null;

alter table student_info
add constraint student_id_pk
primary key (student_id);

alter table student_info
add constraint zip_student_fk
foreign key (zip_code)
references zipcode_info (zip_code);
-----------------section_info---------------------
alter table section_info
alter column section_id numeric(8,0) not null;

alter table section_info
alter column section_no numeric(3,0) not null;

alter table section_info
add constraint section_id_pk
primary key (section_id);

alter table section_info
add constraint course_section_fk
foreign key (course_no)
references course_info (course_no);

alter table section_info
add constraint instructor_section_fk
foreign key (instructor_id)
references instructor_info (instructor_id);
------------------enrollment_info--------------------
alter table enrollment_info
alter column student_id numeric(8,0) not null;

alter table enrollment_info
alter column section_id numeric(8,0) not null;

alter table enrollment_info
add constraint enrollment_stud_sect_pk
primary key (student_id, section_id);

alter table enrollment_info
add constraint enrollment_student_id_fk
foreign key (student_id)
references student_info (student_id);

alter table enrollment_info
add constraint enrollment_section_id_fk
foreign key (section_id)
references section_info (section_id);
----------------grade_info----------------------
alter table grade_info
alter column student_id numeric(8,0) not null;

alter table grade_info
alter column section_id numeric(8,0) not null;

alter table grade_info
alter column grade_type_code char(2) not null;

alter table grade_info
alter column grade_code_occurance numeric(38,0) not null;

alter table grade_info
add constraint grade_stud_sect_type_code_pk
primary key (student_id, section_id, grade_type_code, grade_code_occurance);

alter table grade_info
add constraint grade_student_id_fk
foreign key (student_id)
references student_info (student_id);

alter table grade_info
add constraint grade_section_id_fk
foreign key (section_id)
references section_info (section_id);

alter table grade_info
add constraint chk_grade_type_code
check (grade_type_code in ('fi','hm','mt','pa','pj','qz'));

alter table grade_info
add constraint numeric_grade_nn
default 0 for numeric_grade;




