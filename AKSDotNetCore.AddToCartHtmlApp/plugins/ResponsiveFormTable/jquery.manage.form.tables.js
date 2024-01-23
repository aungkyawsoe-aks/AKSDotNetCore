/**
 * ManageFormTables
 *
 *  jQuery plugin for manager tables from
 *
 * @requires jQuery {@link https://jquery.com/}
 * @author Elbert Tous <elbertjose@hotmail.com>
 */


'use strict';

(function ($, window, document, undefined) {

    /**
     * This plugin's name. Used for namespacing,
     * console errors, jQuery's data.
     *
     * @type {string}
     * @private
     */
    const pluginFnName = 'manageFormTables';


    class ManageFormTables {

        /**
         * Initialize the target element and extend the default options with the
         * caller's parameters; then, start rendering.
         *
         * @param {object} element The target element's identifier.
         * @param {object} options Parameters - defaults extending eventual options.
         * @public
         */
        constructor(element, options) {
            this.element = $(element);
            this.settings = $.extend(true, {}, $.fn[pluginFnName].defaults, options);
            // Store all the console messages.
            this.logs = [];
            this._init();
            this._show();
            this.tableIndexRow = 1;

        }

        /**
         * Start this plugin's logic, such as checking settings,
         * building the expected output, and so on.
         * @private
         */
        _init() {
            this._buildManageFormTables();
        }

        /**
         * Build the alert and populate it if all checks are passed.
         * @private
         */
        _buildManageFormTables() {

            const _this = this;

            _this._configValidationEngine();

            // If no heading and no content are valid, the elements cannot be built.
            let hasSender = true;
            if (!_this._isValidString(_this.settings.senderTarget)) {
                hasSender = false;
                _this._log('no senderTarget provided', 'error');
            }

            let hasRemoveRow = true;
            if (!_this._isValidString(_this.settings.removeRowTarget)) {
                hasRemoveRow = false;
                _this._log('no removeRowTarget provided', 'error');
            }

            let hasAddRow = true;
            if (!this._isValidString(_this.settings.addRowTarget)) {
                hasAddRow = false;
                _this._log('no addRowTarget provided', 'error');
            }

            let hasMinRowsVisible = true;
            if (!_this._isValidNumber(_this.settings.minRowsVisible)) {
                hasMinRowsVisible = false;
                _this._log('no minRowsVisible provided', 'error');
            }

            let hasTemplateRow = true;
            if (!_this._isValidString(_this.settings.templateRow)) {
                hasTemplateRow = false;
                _this._log('no templateRow provided', 'warn');
            }

            let hasValidationEngine = true;
            if (!$.validationEngine.defaults) {
                hasValidationEngine = false;
                _this._log('no validationEngine function provided', 'info');
            }

            let hasOnSubmit = true;
            if (!_this._isValidFunction(_this.settings.onSubmit)) {
                hasOnSubmit = false;
                _this._log('no onSubmit function provided', 'warn');
            }

            let hasOnErrorRowsVisible = true;
            if (!_this._isValidFunction(_this.settings.onErrorRowsVisible)) {
                hasOnErrorRowsVisible = false;
                _this._log('no onErrorRowsVisible function provided', 'warn');
            }

            let hasEvents = true;
            if ($.isArray(_this.settings.events)) {
                $.each(_this.settings.events, function (i, event) {
                    if (!_this._isValidFunction(event.onEvent)) {
                        hasEvents = false;
                        _this._log('no onEvent function provided', 'warn');
                    }
                    if (!_this._isValidString(event.onEventName)) {
                        hasEvents = false;
                        _this._log('no onErrorRowsVisible function provided', 'warn');
                    }
                    if (!_this._isValidString(event.targetName)) {
                        hasEvents = false;
                        _this._log('no onErrorRowsVisible function provided', 'warn');
                    }
                });
            }

            let formTitle = '';
            if (_this._isValidString(_this.settings.tableFormTitle) && _this.settings.tableFormTitle !== '') {
                formTitle = _this.settings.tableFormTitle;
            }

            const $form = _this.element.closest('form');
            _this.element.addClass('manage-table-form');
            $(_this.settings.senderTarget).hide();
            $(_this.settings.addRowTarget, _this.element).on('click', function () {
                if (hasAddRow && hasTemplateRow) {
                    const $currentRowClone = $(_this.settings.templateRow).appendTo(_this.element);
                    const formTitleData = $currentRowClone.closest("table").data('title');
                    if (_this._isValidString(formTitleData) && formTitleData !== '') {
                        formTitle = formTitleData;
                    }
                    if (formTitle !== '') {
                        $currentRowClone.html(function (i, html) {
                            return html.replace(_this.settings.indexRowPattern, formTitle + ' No. ' + _this.tableIndexRow);
                        });
                    }

                    _this.tableIndexRow++;

                    if (hasSender) {
                        $(_this.settings.senderTarget).show();
                    }

                    if (hasRemoveRow) {
                        $(_this.settings.removeRowTarget, $currentRowClone).on('click', function () {
                            const trIndex = $(this).closest("tbody").find('tr').length;
                            if (hasMinRowsVisible) {
                                if (trIndex > _this.settings.minRowsVisible) {
                                    $(this).closest("tr").remove();
                                } else {
                                    if (hasOnErrorRowsVisible) {
                                        _this.settings.onErrorRowsVisible(_this.element, $form);
                                        _this._log('It is not allowed to delete rows less than ' + _this.settings.minRowsVisible, 'error');
                                    }
                                }
                                if (trIndex <= 0) {
                                    $(_this.settings.senderTarget).hide();
                                }
                            } else {
                                $(this).closest("tr").remove();
                            }
                        });
                    }

                    if (hasEvents) {
                        $.each(_this.settings.events, function (i, event) {
                            $(event.targetName, $currentRowClone).on(event.onEventName, event.onEvent);
                        });
                    }

                    return false;
                }
            });

            if (hasSender) {
                $(_this.settings.senderTarget).on('click', function () {
                    if (hasValidationEngine) {
                        $form.validationEngine('attach');
                    }
                });
            }

            if (hasOnSubmit) {
                $form.on('submit', function () {
                    if (hasValidationEngine) {
                        if (!$form.validationEngine('validate')) {
                            return false;
                        }
                    }
                    return _this.settings.onSubmit($form);
                });
            }

        }

        /**
         * single configuration for validation engine
         * @returns {boolean}
         * @private
         */
        _configValidationEngine() {
            if (!$.validationEngine)
                return false;
            $.validationEngine.defaults.promptPosition = 'inline';
            $.validationEngine.defaults.onFieldFailure = function (field) {
                field.removeClass('is-valid').addClass('is-invalid');
            };
            $.validationEngine.defaults.onFieldSuccess = function (field) {
                field.removeClass('is-invalid').addClass('is-valid');
            };
        }

        /**
         * Check if a given function os of the 'function' type
         * and has at least one instruction within its body.
         *
         * @param {function(*=): (*|jQuery|HTMLElement|undefined)} element The element in exam.
         * @returns {boolean} Whether the element is a running function or not.
         * @private
         */
        _isValidFunction(element) {
            // Step one: check the type.
            let isValidType = false;
            if (typeof element === 'function') {
                isValidType = true;
            }

            // Step two: check if there are instructions within the function.
            let hasInstructions = false;
            if (isValidType) {
                const functionBody = element.toString().match(/\{([\s\S]*)\}/m)[1].replace(/^\s*\/\/.*$/mg, '');

                if (functionBody.length > 0) {
                    hasInstructions = true;
                }
            }

            return isValidType && hasInstructions;
        }


        /**
         * Check if a given element is of the 'string' type
         * and contains an actual, non-empty string.
         *
         * @param {mixed} element The element in exam.
         * @returns {boolean} Whether the element is a valid string or not.
         * @private
         */
        _isValidString(element) {
            return typeof element === 'string' && element.trim();
        }


        /**
         * Check if a given element is of the 'number' type
         * and contains an actual, non-empty string.
         *
         * @param {mixed} element The element in exam.
         * @returns {boolean} Whether the element is a valid string or not.
         * @private
         */
        _isValidNumber(element) {
            return typeof element === 'number' && $.isNumeric(element);
        }


        /**
         * Craft a message to send to the browser's console.
         *
         * @param {string} content The message's content.
         * @param {(undefined|string)} type The message's type.
         * @private
         */
        _log(content, type) {
            const defaultType = 'log';
            const allowedTypes = [defaultType, 'error', 'info', 'warn',];

            let messageType = defaultType;
            if (allowedTypes.includes(type)) {
                messageType = type;
            }

            this.logs.push({
                type: messageType,
                content: content,
            });
        }

        /**
         * Print eventual messages that this plugin raised.
         * @private
         */
        _show() {
            if (this.settings.debug === 1) {
                $.each(this.logs, function (i, logs) {
                    console[logs.type](pluginFnName + ': ' + logs.content + '.');
                });
            }
        }


    }


    /**
     * Initialize this plugin for each instanced element.
     *
     * @example
     * $('#tableId').manageFormTables({
     *    templateRow: `
     *                <tr role="row">
     *                   <td role="cell">
     *                        <div>Default Row</div>
     *                   </td>
     *               </tr>
     *              `,
     *    removeRowTarget: '.remove',
     *    addRowTarget: '.add-row',
     *    minRowsVisible: 1,
     *    senderTarget: '.sender',
     *    onSubmit(form) {
     *       // ...
     *    },
     *    onErrorRowsVisible(form) {
     *       // ...
     *    },
     * });
     *
     * @param {object} config Instance options.
     * @returns {(undefined|function)} This plugin's instance.
     * @public
     */
    $.fn[pluginFnName] = function (config) {
        if (typeof config === 'object') {
            return this.each(function (i, target) {
                new ManageFormTables(this, config);
            });
        } else {
            console.error(pluginFnName + ': invalid options.');
        }
    };

    /**
     * This plugin's default attributes and callbacks.
     * Each element can be externally overridden.
     *
     * @example
     * $.fn.manageFormTables.defaults.type = 'danger';
     *
     * @type {object}
     * @public
     */
    $.fn[pluginFnName].defaults = {
        templateRow: `
                    <tr role="row">
                        <td role="cell">
                            <a href="javascript:void(0);" class="btn btn-danger btn-sm remove">
                                <i class="fa fa-times"></i>
                            </a>
                       </td>
                    </tr>   
                  `,
        removeRowTarget: '.remove',
        addRowTarget: '.add-row',
        minRowsVisible: 1,
        tableFormTitle: 'Formulario',
        indexRowPattern: /#i/g,
        debug: 0,
        senderTarget: '.sender',
        onSubmit: function (form) {
        },
        onErrorRowsVisible(element, form) {
        },
        events: [
            {
                targetName: 'edit',
                onEventName: 'click',
                onEvent: function () {
                }
            }
        ]
    };


})(jQuery, window, document);