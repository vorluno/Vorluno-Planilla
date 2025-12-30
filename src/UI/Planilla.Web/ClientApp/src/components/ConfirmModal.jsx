import React from 'react';

const ConfirmModal = ({
    isOpen,
    onClose,
    onConfirm,
    title,
    message,
    confirmText = 'Confirmar',
    cancelText = 'Cancelar',
    variant = 'danger'
}) => {
    if (!isOpen) return null;

    const variants = {
        danger: {
            bgIcon: 'bg-red-100',
            iconColor: 'text-red-600',
            btnBg: 'bg-red-600 hover:bg-red-700',
            icon: (
                <svg className="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
                </svg>
            )
        },
        warning: {
            bgIcon: 'bg-yellow-100',
            iconColor: 'text-yellow-600',
            btnBg: 'bg-yellow-600 hover:bg-yellow-700',
            icon: (
                <svg className="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
                </svg>
            )
        },
        info: {
            bgIcon: 'bg-blue-100',
            iconColor: 'text-blue-600',
            btnBg: 'bg-blue-600 hover:bg-blue-700',
            icon: (
                <svg className="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
            )
        }
    };

    const variantStyle = variants[variant] || variants.danger;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
            <div className="bg-white rounded-xl shadow-2xl max-w-md w-full">
                <div className="p-6">
                    <div className={`w-12 h-12 ${variantStyle.bgIcon} rounded-full flex items-center justify-center mx-auto mb-4`}>
                        <div className={variantStyle.iconColor}>
                            {variantStyle.icon}
                        </div>
                    </div>

                    {title && (
                        <h3 className="text-lg font-semibold text-gray-900 text-center mb-2">
                            {title}
                        </h3>
                    )}

                    {message && (
                        <p className="text-gray-600 text-center mb-6">
                            {message}
                        </p>
                    )}

                    <div className="flex gap-3">
                        <button
                            onClick={onClose}
                            className="flex-1 px-4 py-2 border border-gray-300 rounded-lg text-gray-700 hover:bg-gray-50 font-medium transition-colors"
                        >
                            {cancelText}
                        </button>
                        <button
                            onClick={() => {
                                onConfirm();
                                onClose();
                            }}
                            className={`flex-1 px-4 py-2 ${variantStyle.btnBg} text-white rounded-lg font-medium transition-colors`}
                        >
                            {confirmText}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ConfirmModal;
