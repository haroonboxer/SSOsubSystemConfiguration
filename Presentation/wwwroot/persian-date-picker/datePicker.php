<input class="form-control m-input errorDiv datePicker" type="text" name="start_date_of_contraction" id="start_date_of_contraction" placeholder="{{ trans('home.start_date_of_contraction') }}" style="width: 100%; direction:rtl">




<script>
    $(function () {
        //usage
        $(".datePicker").persianDatepicker({
            cellWidth: 50,
            cellHeight: 35,
            fontSize: 14
        });
        $(".datePicker").focus(function () {
            $(this).blur();
        });
    });

</script>